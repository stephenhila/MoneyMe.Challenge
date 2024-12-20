using Microsoft.EntityFrameworkCore;

namespace MoneyMe.Challenge.Data;

public interface ILoanContext
{
    DbSet<LoanApplication> LoanApplications { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
