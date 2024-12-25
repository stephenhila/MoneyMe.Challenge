namespace MoneyMe.Challenge.Data;

public class LoanApplication
{
    public Guid Id { get; set; }
    public decimal AmountRequired { get; set; }
    public int Term { get; set; }
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string? Product { get; set; }
    public double? AnnualInterestRate { get; set; }
    public int? InterestFreeGracePeriodMonths { get; set; }
    public double? PMT { get; set; }
    public double? PMTWithoutInterest { get; set; }
}
