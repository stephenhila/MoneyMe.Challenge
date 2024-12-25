using MoneyMe.Challenge.Business.Enums;
using System.ComponentModel.DataAnnotations;

namespace MoneyMe.Challenge.Business.DTO;

public class LoanApplicationDTO
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "enter amount")]
    public decimal AmountRequired { get; set; }
    [Required(ErrorMessage = "enter term (in months)")]
    public int Term { get; set; }
    [Required(ErrorMessage = "enter a title")]
    public string Title { get; set; }
    [Required(ErrorMessage = "enter your first name")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "enter your last name")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "enter your date of birth")]
    public DateOnly DateOfBirth { get; set; }
    [Required(ErrorMessage = "enter your mobile number")]
    public string Mobile { get; set; }
    [Required(ErrorMessage = "enter your email address")]
    [EmailAddress(ErrorMessage = "enter a valid email address")]
    public string Email { get; set; }
    public string? Product { get; set; }
    public double? AnnualInterestRate { get; set; }
    public int? InterestFreeGracePeriodMonths { get; set; }
    public double? PMT { get; set; }
    public double? PMTWithoutInterest { get; set; }
}
