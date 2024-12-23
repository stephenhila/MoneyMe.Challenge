using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Web.UI.Services;
using Newtonsoft.Json;
using System.Text;

namespace MoneyMe.Challenge.Web.UI.Pages
{
    public class QuoteCalculatorModel : PageModel
    {
        [BindProperty]
        public LoanApplicationDTO LoanApplication { get; set; }

        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }

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

            if (LoanApplication == null)
            {
                return NotFound();
            }

            MinValue = Math.Min(2100, LoanApplication.AmountRequired);
            MaxValue = Math.Max(10000, LoanApplication.AmountRequired);

            return Page();
        }
    }
}
