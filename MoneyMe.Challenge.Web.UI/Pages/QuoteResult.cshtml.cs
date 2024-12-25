using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Business.Queries;
using MoneyMe.Challenge.Web.UI.Extensions;


namespace MoneyMe.Challenge.Web.UI.Pages
{
    public class QuoteResultModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public LoanApplicationDTO LoanApplication { get; set; }

        [TempData]
        public string PMTWIthoutInterest { get; set; }

        [TempData]
        public string PMT { get; set; }

        public QuoteResultModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            LoanApplication = HttpContext.Session.Get<LoanApplicationDTO>("LoanApplication");

            var result = await _mediator.Send(new CalculatePMTQuery { PrincipalAmount = (double)LoanApplication.AmountRequired, NumberOfPayments = LoanApplication.Term, AnnualInterestRate = 3.5, GracePeriodMonths = 2 });

            PMT = result.PMT.ToString("F2");
            PMTWIthoutInterest = result.PMTWithoutInterest.ToString("F2");
            return Page();
        }
    }
}
