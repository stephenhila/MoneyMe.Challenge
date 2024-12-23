using MoneyMe.Challenge.Business.Commands;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Challenge.Business.Tests.Commands;

public class UpdateLoanApplicationCommandHandlerTestsFixture : BaseLoanRequestHandlerTestFixture<UpdateLoanApplicationCommand, bool>
{
    public UpdateLoanApplicationCommandHandlerTestsFixture()
    {
        Handler = new UpdateLoanApplicationCommandHandler(LoanContext, Mapper);

        // Arrange test-wide shared data
        LoanApplication existingLoanApplication = new LoanApplication
        {
            Id = new Guid("33333333-3333-3333-3333-333333333333"),
            Title = "Ms.",
            FirstName = "Jenifer",
            LastName = "Dough",
            DateOfBirth = new DateOnly(2003, 03, 03),
            Email = "jenifer.dough@email.com",
            Mobile = "3333333",
            Term = 3,
            AmountRequired = 3000
        };

        LoanContext.LoanApplications.Add(existingLoanApplication);
        LoanContext.SaveChanges();
    }
}

[Collection("UpdateLoanApplicationCommandHandlerTestsCollection")]
public class UpdateLoanApplicationCommandHandlerTests
{
    private readonly UpdateLoanApplicationCommandHandlerTestsFixture _fixture;
    public UpdateLoanApplicationCommandHandlerTests(UpdateLoanApplicationCommandHandlerTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_ExistingApplication_ReturnsTrue()
    {
        // Arrange
        LoanApplicationDTO updatedLoanApplication = new LoanApplicationDTO
        {
            Title = "Mrs.",
            FirstName = "Jenifer",
            LastName = "Dough",
            DateOfBirth = new DateOnly(2003, 03, 03),
            Email = "married.jenifer.dough@email.com",
            Mobile = "2-3333333",
            Term = 4,
            AmountRequired = 4000
        };

        var command = new UpdateLoanApplicationCommand { Id = "33333333-3333-3333-3333-333333333333", LoanApplication = updatedLoanApplication };

        // Act
        var result = await _fixture.Handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Handle_NonExistingApplication_ReturnsFalse()
    {
        // Arrange
        LoanApplicationDTO updatedLoanApplication = new LoanApplicationDTO
        {
            Title = "Mrs.",
            FirstName = "Jenifer",
            LastName = "Dough",
            DateOfBirth = new DateOnly(2003, 03, 03),
            Email = "married.jenifer.dough@email.com",
            Mobile = "2-3333333",
            Term = 4,
            AmountRequired = 4000
        };

        var command = new UpdateLoanApplicationCommand { Id = "30000003-3003-3003-3003-300000000003", LoanApplication = updatedLoanApplication };

        // Act
        var result = await _fixture.Handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result);
    }
}

[CollectionDefinition("UpdateLoanApplicationCommandHandlerTestsCollection")]
public class UpdateLoanApplicationCommandHandlerTestsCollection : ICollectionFixture<UpdateLoanApplicationCommandHandlerTestsFixture>
{
}