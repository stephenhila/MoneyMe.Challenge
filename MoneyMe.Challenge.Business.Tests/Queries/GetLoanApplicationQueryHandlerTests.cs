using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MoneyMe.Challenge.Business.Commands;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Business.Queries;
using MoneyMe.Challenge.Business.Tests.Commands;
using MoneyMe.Challenge.Data;

namespace MoneyMe.Challenge.Business.Tests.Queries;

public class GetLoanApplicationQueryHandlerTestsFixture : BaseLoanRequestHandlerTestFixture<GetLoanApplicationQuery, LoanApplicationDTO>
{
    public GetLoanApplicationQueryHandlerTestsFixture() 
        : base()
    {
        Handler = new GetLoanApplicationQueryHandler(LoanContext, Mapper);

        // Arrange test-wide shared data
        LoanApplication existingLoanApplication = new LoanApplication
        {
            Id = new Guid("22222222-2222-2222-2222-222222222222"),
            Title = "Ms.",
            FirstName = "Jane",
            LastName = "Doe",
            DateOfBirth = new DateOnly(2002, 02, 02),
            Email = "jane.doe@email.com",
            Mobile = "2222222",
            Term = 2,
            AmountRequired = 20000
        };

        LoanContext.LoanApplications.Add(existingLoanApplication);
        LoanContext.SaveChanges();
    }
}

[Collection("GetLoanApplicationQueryHandlerTestsCollection")]
public class GetLoanApplicationQueryHandlerTests
{
    private readonly GetLoanApplicationQueryHandlerTestsFixture _fixture;

    public GetLoanApplicationQueryHandlerTests(GetLoanApplicationQueryHandlerTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_ExistingApplication_ReturnsExistingApplication()
    {

        // Arrange
        LoanApplicationDTO expectedLoanApplication = new LoanApplicationDTO
        {
            Title = "Ms.",
            FirstName = "Jane",
            LastName = "Doe",
            DateOfBirth = new DateOnly(2002, 02, 02),
            Email = "jane.doe@email.com",
            Mobile = "2222222",
            Term = 2,
            AmountRequired = 20000
        };

        var command = new GetLoanApplicationQuery { Id = "22222222-2222-2222-2222-222222222222" };

        // Act
        var result = await _fixture.Handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(expectedLoanApplication.FirstName, result.FirstName, ignoreCase: true);
        Assert.Equal(expectedLoanApplication.LastName, result.LastName, ignoreCase: true);
        Assert.True(expectedLoanApplication.DateOfBirth == result.DateOfBirth);
    }
}

[CollectionDefinition("GetLoanApplicationQueryHandlerTestsCollection")]
public class GetLoanApplicationQueryHandlerTestsCollection : ICollectionFixture<GetLoanApplicationQueryHandlerTestsFixture>
{
}