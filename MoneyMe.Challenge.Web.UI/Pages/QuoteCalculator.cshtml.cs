using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneyMe.Challenge.Business.Commands;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Business.Enums;
using MoneyMe.Challenge.Business.Queries;
using MoneyMe.Challenge.Web.UI.Extensions;
using MoneyMe.Challenge.Web.UI.Services;

namespace MoneyMe.Challenge.Web.UI.Pages
{
    public class QuoteCalculatorModel : PageModel
    {
        [BindProperty]
        public LoanApplicationDTO LoanApplication { get; set; }

        public string SelectedProductDescription { get; set; }

        public decimal MinAmountRequiredValue { get; set; }
        public decimal MaxAmountRequiredValue { get; set; }
        public decimal MinTermValue { get; set; }
        public decimal MaxTermValue { get; set; }

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILoanApplicationService _loanApplicationService;
        private readonly ILogger<QuoteCalculatorModel> _logger;
        private readonly IConfiguration _configuration;

        public QuoteCalculatorModel(IMediator mediator, IMapper mapper, ILoanApplicationService loanApplicationService, ILogger<QuoteCalculatorModel> logger, IConfiguration configuration)
        {
            _mediator = mediator;
            _mapper = mapper;
            _loanApplicationService = loanApplicationService;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            LoanApplication = _mapper.Map<LoanApplicationDTO>(await _loanApplicationService.GetLoanApplicationAsync(id));

            if (LoanApplication == null)
            {
                return NotFound();
            }

            MinAmountRequiredValue = Math.Min(2100, LoanApplication.AmountRequired);
            MaxAmountRequiredValue = Math.Max(10000, LoanApplication.AmountRequired);

            MinTermValue = Math.Min(1, LoanApplication.Term);
            MaxTermValue = Math.Max(12, LoanApplication.Term);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // do calculations based on selected product
            LoanApplication.AnnualInterestRate = await _mediator.Send(new CalculateAnnualInterestRateByProductQuery { Product = Enum.Parse<Product>(LoanApplication.Product) });
            LoanApplication.InterestFreeGracePeriodMonths = await _mediator.Send(new CalculateGracePeriodMonthsByProductQuery { Product = Enum.Parse<Product>(LoanApplication.Product) });
            var pmtCalculation = await _mediator.Send(new CalculatePMTQuery { PrincipalAmount = (double)LoanApplication.AmountRequired, NumberOfPayments = LoanApplication.Term, AnnualInterestRate = LoanApplication.AnnualInterestRate.Value, GracePeriodMonths = LoanApplication.InterestFreeGracePeriodMonths.Value });
            LoanApplication.PMT = pmtCalculation.PMT;
            LoanApplication.PMTWithoutInterest = pmtCalculation.PMTWithoutInterest;

            // update the LoanApplication data
            //var loanApplicationUpdated = await _mediator.Send(new UpdateLoanApplicationCommand { Id = LoanApplication.Id.ToString(), LoanApplication = LoanApplication });
            var loanApplicationUpdated = await _loanApplicationService.UpdateLoanApplicationAsync(LoanApplication);


            // redirect to next page
            if (loanApplicationUpdated)
                return RedirectToPage("quoteresult", new { id = LoanApplication.Id });
            else
                return StatusCode(500, "An error has occured while processing your request.");
        }

        public async Task<JsonResult> OnPostProductSelected([FromBody] string selectedProduct)
        {
            if (Enum.TryParse<Product>(selectedProduct, out var selectedProductEnum))
            {
                LoanApplication.Product = selectedProductEnum.ToString();

                // Update the range slider limits based on the selected product
                MinTermValue = await _mediator.Send(new CalculateMinimumTermByProductQuery { Product = Enum.Parse<Product>(LoanApplication.Product) });
                SelectedProductDescription = await _mediator.Send(new GenerateProductDescriptionByProductQuery { Product = Enum.Parse<Product>(LoanApplication.Product) });
                return new JsonResult(new { desc = SelectedProductDescription, min = MinTermValue });
            }

            // Handle error case if the string cannot be parsed to a valid enum value
            return new JsonResult(new { error = "Invalid product selected." });
        }
    }
}
