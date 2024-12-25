using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Business.Queries;
using MoneyMe.Challenge.Web.UI.Extensions;
using MoneyMe.Challenge.Web.UI.Services;
using System.Text;
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
            StringBuilder sb = new StringBuilder();
            TempData["ErrorMessage"] = string.Empty;

            var loanApplicationJson = HttpContext.Session.GetString("LoanApplication");
            if (!string.IsNullOrEmpty(loanApplicationJson))
            {
                LoanApplication = JsonSerializer.Deserialize<LoanApplicationDTO>(loanApplicationJson);
            }

            bool isMobileBlacklisted = false;

            try
            {
                var mobileBlacklistResult = await _blacklistService.GetMobileNumberBlacklistAsync(LoanApplication.Mobile);
                isMobileBlacklisted = mobileBlacklistResult != null && mobileBlacklistResult.Mobile == LoanApplication.Mobile;
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("404"))
                    throw ex;
            }

            bool isEmailBlacklisted = false;
            try
            {
                var emailBlacklistResult = await _blacklistService.GetEmailBlacklistAsync(LoanApplication.Email);
                isEmailBlacklisted = emailBlacklistResult != null && emailBlacklistResult.Email == LoanApplication.Email;
            }catch (Exception ex)
            {
                if (!ex.Message.Contains("404"))
                    throw ex;
            }

            bool isAgeNotAllowed = false;
            // Check if the applicant is below 18
            if (LoanApplication.DateOfBirth.AddYears(18) > DateOnly.FromDateTime(DateTime.Now))
            {
                ModelState.AddModelError(string.Empty, "You must be at least 18 years old to apply for a loan.");
                isAgeNotAllowed = true;
            }

            // Check if the applicant's mobile number is blacklisted
            if (isMobileBlacklisted)
            {
                ModelState.AddModelError(nameof(LoanApplication.Mobile), "The provided mobile number has been blacklisted.");
            }

            // Check if the applicant's mobile number is blacklisted
            if (isEmailBlacklisted)
            {
                ModelState.AddModelError(nameof(LoanApplication.Email), "The provided email has been blacklisted.");
            }

            // If there are validation errors, redisplay the page with error messages
            if (isEmailBlacklisted || isMobileBlacklisted || isAgeNotAllowed)
            {
                sb.Append("Cannot proceed with application!");
                if (isEmailBlacklisted) sb.Append("|The provided email has been blacklisted.");
                if (isMobileBlacklisted) sb.Append("|The provided mobile number has been blacklisted.");
                if (isAgeNotAllowed) sb.Append("|You must be at least 18 years old to apply for a loan.");
                TempData["ErrorMessage"] = sb.ToString();
                return RedirectToPage("loanapplicationfailed", new { errors = sb.ToString() });
            }

            // If all validations pass, proceed with the logic (e.g., save data, redirect, etc.)
            // Perform saving or processing logic here...

            return RedirectToPage("loanapplicationsuccess");
        }
    }
}
