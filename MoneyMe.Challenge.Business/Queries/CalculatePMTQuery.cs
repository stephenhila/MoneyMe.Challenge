using MediatR;

namespace MoneyMe.Challenge.Business.Queries;

public class CalculatePMTQueryHandler : IRequestHandler<CalculatePMTQuery, double>
{
    public Task<double> Handle(CalculatePMTQuery request, CancellationToken cancellationToken)
    {
        if (request.AnnualInterestRate <= 0 || request.NumberOfPayments <= 0)
        {
            throw new ArgumentException("Invalid input: Annual interest rate and number of payments must be greater than zero.");
        }

        var monthlyInterestRate = request.AnnualInterestRate / 12 / 100;

        var monthlyPayment = request.PrincipalAmount *
                (monthlyInterestRate / (1 - Math.Pow((1 + monthlyInterestRate), -request.NumberOfPayments)));

        return Task.FromResult(Math.Round(monthlyPayment, 2)); // Round to two decimal places
    }
}

public class CalculatePMTQuery : IRequest<double>
{
    public double PrincipalAmount { get; set; }
    public double AnnualInterestRate { get; set; }
    public int NumberOfPayments { get; set; }
}
