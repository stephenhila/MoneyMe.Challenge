using MediatR;
using MoneyMe.Challenge.Business.Enums;

namespace MoneyMe.Challenge.Business.Queries;

public class CalculateGracePeriodMonthsByProductQueryHandler : IRequestHandler<CalculateGracePeriodMonthsByProductQuery, int>
{
    public Task<int> Handle(CalculateGracePeriodMonthsByProductQuery request, CancellationToken cancellationToken)
    {
        int gracePeriodMonths;

        if (request.Product == Product.ProductB)
            gracePeriodMonths = 2;
        else
            gracePeriodMonths = 0;

        return Task.FromResult(gracePeriodMonths);
    }
}

public class CalculateGracePeriodMonthsByProductQuery : IRequest<int>
{
    public Product Product { get; set; }
}
