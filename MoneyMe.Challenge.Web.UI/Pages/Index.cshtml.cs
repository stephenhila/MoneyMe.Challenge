using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Web.UI.Services;
using Newtonsoft.Json;
using System.Text;

namespace MoneyMe.Challenge.Web.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILoanApplicationService _loanApplicationService;
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public LoanApplicationDTO LoanApplication { get; set; }

        public IndexModel(ILoanApplicationService loanApplicationService, ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _loanApplicationService = loanApplicationService;
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Handle validation errors
                return Page();
            }

            var redirectUrl = await _loanApplicationService.SubmitAndGetLoanApplicationRedirectUrlAsync(LoanApplication);

            return Redirect(redirectUrl);
        }
    }
}
