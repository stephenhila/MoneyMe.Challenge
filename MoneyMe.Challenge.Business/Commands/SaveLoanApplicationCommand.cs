using AutoMapper;
using MediatR;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Data;
using SQLitePCL;

namespace MoneyMe.Challenge.Business.Commands;

public class SaveLoanApplicationCommandHandler : IRequestHandler<SaveLoanApplicationCommand, Guid>
{
    private readonly LoanContext _context;
    private readonly IMapper _mapper;

    public SaveLoanApplicationCommandHandler(LoanContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

    public async Task<Guid> Handle(SaveLoanApplicationCommand request, CancellationToken cancellationToken)
    {
        var loanApplication = _mapper.Map<LoanApplication>(request.LoanApplication);

        _context.LoanApplications.Add(loanApplication);
        await _context.SaveChangesAsync(cancellationToken);

        return loanApplication.Id;
    }
}

public class SaveLoanApplicationCommand : IRequest<Guid>
{
    public LoanApplicationDTO LoanApplication { get; set; }
}