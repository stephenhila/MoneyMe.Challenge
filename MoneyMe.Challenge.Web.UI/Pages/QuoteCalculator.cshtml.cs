using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneyMe.Challenge.Business.DTO;

namespace MoneyMe.Challenge.Web.UI.Pages
{
    public class QuoteCalculatorModel : PageModel
    {
        [BindProperty]
        public LoanApplicationDTO LoanApplication { get; set; }

        public void OnGet()
        {
        }
    }
}
