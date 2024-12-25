using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Business.Queries;
using MoneyMe.Challenge.Web.UI.Extensions;
using MoneyMe.Challenge.Web.UI.Services;


namespace MoneyMe.Challenge.Web.UI.Pages
{
    public class QuoteResultModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ILoanApplicationService _loanApplicationService;

        [BindProperty]
        public LoanApplicationDTO LoanApplication { get; set; }

        public QuoteResultModel(IMediator mediator, ILoanApplicationService loanApplicationService)
        {
            _mediator = mediator;
            _loanApplicationService = loanApplicationService;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            LoanApplication = await _loanApplicationService.GetLoanApplicationAsync(id);
            return Page();
        }
    }
}
