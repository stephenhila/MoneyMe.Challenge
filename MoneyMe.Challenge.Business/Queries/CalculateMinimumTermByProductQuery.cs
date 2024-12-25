using MediatR;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Business.Enums;

namespace MoneyMe.Challenge.Business.Queries;

public class CalculateMinimumTermByProductQueryHandler : IRequestHandler<CalculateMinimumTermByProductQuery, int>
{
    public Task<int> Handle(CalculateMinimumTermByProductQuery request, CancellationToken cancellationToken)
    {
        int minimumTerm = 1;

        if (request.Product == Product.ProductB)
            minimumTerm = 6;

        return Task.FromResult(minimumTerm);
    }
}

public class CalculateMinimumTermByProductQuery : IRequest<int>
{
    public Product Product { get; set; }
}
