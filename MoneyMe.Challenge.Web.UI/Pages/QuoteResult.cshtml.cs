using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Business.Queries;
using MoneyMe.Challenge.Web.UI.Extensions;
using MoneyMe.Challenge.Web.UI.Services;
using System.Text.Json;


namespace MoneyMe.Challenge.Web.UI.Pages
{
    public class QuoteResultModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ILoanApplicationService _loanApplicationService;
        private readonly IBlacklistService _blacklistService;

        [BindProperty]
        public LoanApplicationDTO LoanApplication { get; set; }

        public QuoteResultModel(IMediator mediator, ILoanApplicationService loanApplicationService, IBlacklistService blacklistService)
        {
            _mediator = mediator;
            _loanApplicationService = loanApplicationService;
            _blacklistService = blacklistService;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            LoanApplication = await _loanApplicationService.GetLoanApplicationAsync(id);
            HttpContext.Session.SetString("LoanApplication", JsonSerializer.Serialize(LoanApplication));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var loanApplicationJson = HttpContext.Session.GetString("LoanApplication");
            if (!string.IsNullOrEmpty(loanApplicationJson))
            {
                LoanApplication = JsonSerializer.Deserialize<LoanApplicationDTO>(loanApplicationJson);
            }

            var mobileBlacklistResult = await _blacklistService.GetMobileNumberBlacklistAsync(LoanApplication.Mobile);
            var emailBlacklistResult = await _blacklistService.GetEmailBlacklistAsync(LoanApplication.Email);

            throw new NotImplementedException();
        }
    }
}
