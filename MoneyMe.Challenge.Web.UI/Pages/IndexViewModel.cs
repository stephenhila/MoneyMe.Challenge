using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MoneyMe.Challenge.Web.UI.Pages;

public class IndexViewModel
{
    public decimal AmountRequired { get; set; }
    public int Term { get; set; }
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string SuccessMessage { get; set; }
    public string ErrorMessage { get; set; }
}
