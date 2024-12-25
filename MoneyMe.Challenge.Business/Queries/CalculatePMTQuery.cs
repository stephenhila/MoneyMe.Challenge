using MediatR;
using MoneyMe.Challenge.Business.DTO;

namespace MoneyMe.Challenge.Business.Queries;

public class CalculatePMTQueryHandler : IRequestHandler<CalculatePMTQuery, CalculatePMTResultDTO>
{
    public Task<CalculatePMTResultDTO> Handle(CalculatePMTQuery request, CancellationToken cancellationToken)
    {
        // Interest-free payment calculation
        double interestFreePayment = request.PrincipalAmount / request.NumberOfPayments * request.GracePeriodMonths;
        double pmtWithoutInterest = interestFreePayment / request.GracePeriodMonths;


        // Adjust the principal for the grace period
        double adjustedPrincipal = request.PrincipalAmount - interestFreePayment;

        // Convert the annual interest rate to the periodic interest rate
        double periodicRate = request.AnnualInterestRate / 100 / 12; // Assuming monthly payments

        // Calculate PMT for the remaining payments
        int remainingPayments = request.NumberOfPayments - request.GracePeriodMonths;

        // Regular PMT formula for the adjusted principal
        double pmt = (adjustedPrincipal * periodicRate * Math.Pow(1 + periodicRate, remainingPayments)) /
                    (Math.Pow(1 + periodicRate, remainingPayments) - 1);

        return Task.FromResult(new CalculatePMTResultDTO { PMT = pmt, PMTWithoutInterest = pmtWithoutInterest });
    }
}

public class CalculatePMTQuery : IRequest<CalculatePMTResultDTO>
{
    public double PrincipalAmount { get; set; }
    public double AnnualInterestRate { get; set; }
    public int NumberOfPayments { get; set; }
    public int GracePeriodMonths { get; set; }
}
