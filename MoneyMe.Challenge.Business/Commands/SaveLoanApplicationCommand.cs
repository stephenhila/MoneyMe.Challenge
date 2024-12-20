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
        var existingLoanApplication = _context.LoanApplications.FirstOrDefault(la =>
                    la.FirstName.Equals(request.LoanApplication.FirstName, StringComparison.OrdinalIgnoreCase)
                 && la.LastName.Equals(request.LoanApplication.LastName, StringComparison.OrdinalIgnoreCase)
                 && la.DateOfBirth.Equals(request.LoanApplication.DateOfBirth)
            );

        if (existingLoanApplication != default)
        {
            return existingLoanApplication.Id;
        }

        var newLoanApplication = _mapper.Map<LoanApplication>(request.LoanApplication);
        _context.LoanApplications.Add(newLoanApplication);
        await _context.SaveChangesAsync(cancellationToken);
        return newLoanApplication.Id;
    }
}

public class SaveLoanApplicationCommand : IRequest<Guid>
{
    public LoanApplicationDTO LoanApplication { get; set; }
}