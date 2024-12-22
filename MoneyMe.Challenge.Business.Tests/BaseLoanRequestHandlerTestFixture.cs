using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyMe.Challenge.Business.Mappings;
using MoneyMe.Challenge.Data;
namespace MoneyMe.Challenge.Business.Tests;

public abstract class BaseLoanRequestHandlerTestFixture<TRequest, TResponse> : IDisposable, IBaseRequest where TRequest : IRequest<TResponse>
{
    public required IRequestHandler<TRequest, TResponse> Handler {  get; set; }
    public required LoanContext LoanContext { get; set; }
    public required IMapper Mapper { get; set; }

    public BaseLoanRequestHandlerTestFixture()
    {
        var options = new DbContextOptionsBuilder<LoanContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        LoanContext = new LoanContext(options);

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<LoanApplicationMappingProfile>();
        });

        Mapper = configuration.CreateMapper();

    }

    public void Dispose()
    {
        LoanContext.Dispose();
    }
}
