using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyMe.Challenge.Business.Commands;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Data;

namespace MoneyMe.Challenge.Business.Queries;

public class GetLoanApplicationQueryHandler : IRequestHandler<GetLoanApplicationQuery, LoanApplicationDTO>
{
    private readonly LoanContext _context;
    private readonly IMapper _mapper;

    public GetLoanApplicationQueryHandler(LoanContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

    public async Task<LoanApplicationDTO> Handle(GetLoanApplicationQuery request, CancellationToken cancellationToken)
    {
        Guid loanRequestId = new Guid(request.Id);
        var loanApplication = await _context.LoanApplications.FirstOrDefaultAsync(loan => loan.Id == loanRequestId);

        return _mapper.Map<LoanApplicationDTO>(loanApplication);
    }
}

public class GetLoanApplicationQuery : IRequest<LoanApplicationDTO>
{
    public string Id { get; set; }
}
