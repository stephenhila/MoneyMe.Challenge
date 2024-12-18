using Microsoft.EntityFrameworkCore;

namespace MoneyMe.Challenge.Data;

public class LoanContext : DbContext
{
    public LoanContext(DbContextOptions<LoanContext> options) : base(options) { }

    public DbSet<LoanApplication> LoanApplications { get; set; }
}
