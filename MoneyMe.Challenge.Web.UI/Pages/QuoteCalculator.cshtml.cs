using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Web.UI.Extensions;
using MoneyMe.Challenge.Web.UI.Services;

namespace MoneyMe.Challenge.Web.UI.Pages
{
    public class QuoteCalculatorModel : PageModel
    {
        [BindProperty]
        public LoanApplicationDTO LoanApplication { get; set; }
        [TempData]
        public Guid LoanApplicationId { get; set; }

        public decimal MinAmountRequiredValue { get; set; }
        public decimal MaxAmountRequiredValue { get; set; }
        public decimal MinTermValue { get; set; }
        public decimal MaxTermValue { get; set; }

        private readonly ILoanApplicationService _loanApplicationService;
        private readonly ILogger<QuoteCalculatorModel> _logger;
        private readonly IConfiguration _configuration;

        public QuoteCalculatorModel(ILoanApplicationService loanApplicationService, ILogger<QuoteCalculatorModel> logger, IConfiguration configuration)
        {
            _loanApplicationService = loanApplicationService;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            LoanApplication = await _loanApplicationService.GetLoanApplicationAsync(id);
            LoanApplicationId = id;

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
            HttpContext.Session.Set("LoanApplication", LoanApplication);
            return RedirectToPage("quoteresult", new { id = LoanApplicationId });
        }
    }
}
