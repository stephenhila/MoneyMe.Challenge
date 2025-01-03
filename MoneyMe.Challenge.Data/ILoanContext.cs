﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MoneyMe.Challenge.Data;

public interface ILoanContext
{
    DbSet<LoanApplication> LoanApplications { get; set; }
    DbSet<MobileNumberBlacklist> MobileNumberBlacklist { get; set; }
    DbSet<EmailBlacklist> EmailBlacklist { get; set; }

    EntityEntry Entry(object entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
