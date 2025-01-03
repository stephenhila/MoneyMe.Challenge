﻿using Microsoft.EntityFrameworkCore;

namespace MoneyMe.Challenge.Data;

public class LoanContext : DbContext, ILoanContext
{
    public LoanContext(DbContextOptions<LoanContext> options) : base(options) { }

    public DbSet<LoanApplication> LoanApplications { get; set; }
    public DbSet<MobileNumberBlacklist> MobileNumberBlacklist { get; set; }
    public DbSet<EmailBlacklist> EmailBlacklist { get; set; }
}
