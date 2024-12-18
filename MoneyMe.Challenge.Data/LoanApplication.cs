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
}
