using MediatR;
using MoneyMe.Challenge.Business.Enums;

namespace MoneyMe.Challenge.Business.Queries;

public class GenerateProductDescriptionByProductQueryHandler : IRequestHandler<GenerateProductDescriptionByProductQuery, string>
{
    public Task<string> Handle(GenerateProductDescriptionByProductQuery request, CancellationToken cancellationToken)
    {
        string productDescription = string.Empty;
        switch(request.Product)
        {
            case Product.ProductA:
                productDescription = $"{request.Product.ToString()} has a loan that is interest-free";
                break;
            case Product.ProductB:
                productDescription = $"{request.Product.ToString()} has a minimum term of 6 months, the first 2 months are interest free";
                break;
            case Product.ProductC:
                productDescription = $"{request.Product.ToString()} has a loan that has interest";
                break;
        }

        return Task.FromResult(productDescription);
    }
}

public class GenerateProductDescriptionByProductQuery : IRequest<string>
{
    public Product Product { get; set; }
}
