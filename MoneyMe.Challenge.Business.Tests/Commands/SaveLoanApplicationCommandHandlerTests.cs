using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyMe.Challenge.Business.Commands;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Business.Mappings;
using MoneyMe.Challenge.Data;

namespace MoneyMe.Challenge.Business.Tests.Commands;


public class SaveLoanApplicationCommandHandlerTestsFixture : BaseLoanRequestHandlerTestFixture<SaveLoanApplicationCommand, Guid>
{
    public SaveLoanApplicationCommandHandlerTestsFixture()
    {
        Handler = new SaveLoanApplicationCommandHandler(LoanContext, Mapper);

        // Arrange test-wide shared data
        LoanApplication existingLoanApplication = new LoanApplication
        {
            Id = new Guid("11111111-1111-1111-1111-111111111111"),
            Title = "Mr.",
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateOnly(2001, 01, 01),
            Email = "john.doe@email.com",
            Mobile = "1111111",
            Term = 1,
            AmountRequired = 10000
        };

        LoanContext.LoanApplications.Add(existingLoanApplication);
        LoanContext.SaveChanges();
    }
}

[Collection("SaveLoanApplicationCommandHandlerTestsCollection")]
public class SaveLoanApplicationCommandHandlerTests
{
    private readonly SaveLoanApplicationCommandHandlerTestsFixture _fixture;

    public SaveLoanApplicationCommandHandlerTests(SaveLoanApplicationCommandHandlerTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_ExistingApplication_ReturnsExistingId()
    {
        // Arrange
        LoanApplicationDTO loanApplication = new LoanApplicationDTO
        {
            Title = "Mr.",
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateOnly(2001,01,01),
            Email = "john.doe@email.com",
            Mobile = "1111111",
            Term = 1,
            AmountRequired = 10000
        };

        var command = new SaveLoanApplicationCommand { LoanApplication = loanApplication };

        // Act
        var result = await _fixture.Handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(result, new Guid("11111111-1111-1111-1111-111111111111"));
    }

    [Fact]
    public async Task Handle_NewApplication_ReturnsUniqueId()
    {
        // Arrange
        LoanApplicationDTO loanApplication = new LoanApplicationDTO
        {
            Title = "Ms.",
            FirstName = "Jane",
            LastName = "Doe",
            DateOfBirth = new DateOnly(2002, 01, 01),
            Email = "jane.doe@email.com",
            Mobile = "2222222",
            Term = 2,
            AmountRequired = 20000
        };

        var command = new SaveLoanApplicationCommand { LoanApplication = loanApplication };

        // Act
        var result = await _fixture.Handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(result, default);
    }
}


[CollectionDefinition("SaveLoanApplicationCommandHandlerTestsCollection")]
public class SaveLoanApplicationCommandHandlerTestsCollection : ICollectionFixture<SaveLoanApplicationCommandHandlerTestsFixture>
{
}