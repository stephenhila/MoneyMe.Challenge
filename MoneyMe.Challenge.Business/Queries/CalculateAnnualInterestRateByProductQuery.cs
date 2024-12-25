using MediatR;
using MoneyMe.Challenge.Business.Enums;

namespace MoneyMe.Challenge.Business.Queries;

public class CalculateAnnualInterestRateByProductQueryHandler : IRequestHandler<CalculateAnnualInterestRateByProductQuery, double>
{
    public Task<double> Handle(CalculateAnnualInterestRateByProductQuery request, CancellationToken cancellationToken)
    {
        double annualInterestRate;

        if (request.Product == Product.ProductA)
            annualInterestRate = 0;
        else
            annualInterestRate = 3.5;

        return Task.FromResult(annualInterestRate);
    }
}

public class CalculateAnnualInterestRateByProductQuery : IRequest<double>
{
    public Product Product { get; set; }
}
