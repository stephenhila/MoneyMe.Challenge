using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MoneyMe.Challenge.Web.UI.Pages
{
    public class LoanApplicationFailedModel : PageModel
    {
        public List<string> Errors { get; set; } = new List<string>();

        public void OnGet()
        {
            // Retrieve errors from TempData and split them into a list
            if (TempData["ErrorMessage"] != null)
            {
                Errors = TempData["ErrorMessage"].ToString().Split('|').ToList();
            }
        }
    }
}
