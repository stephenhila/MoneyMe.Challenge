using MediatR;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Data;
using SQLitePCL;

namespace MoneyMe.Challenge.Business.Commands;

public class SaveLoanApplicationCommandHandler : IRequestHandler<SaveLoanApplicationCommand, Guid>
{
    private readonly LoanContext _context;

    public SaveLoanApplicationCommandHandler(LoanContext context) => _context = context;

    public async Task<Guid> Handle(SaveLoanApplicationCommand request, CancellationToken cancellationToken)
    {
        var loanApplication = new LoanApplication // Replace with your actual model class
        {
            AmountRequired = request.LoanApplication.AmountRequired,
            Term = request.LoanApplication.Term,
            Title = request.LoanApplication.Title,
            FirstName = request.LoanApplication.FirstName,
            LastName = request.LoanApplication.LastName,
            DateOfBirth = request.LoanApplication.DateOfBirth,
            Mobile = request.LoanApplication.Mobile,
            Email = request.LoanApplication.Email
        };

        _context.LoanApplications.Add(loanApplication);
        await _context.SaveChangesAsync(cancellationToken);

        return loanApplication.Id;
    }
}

public class SaveLoanApplicationCommand : IRequest<Guid>
{
    public LoanApplicationDTO LoanApplication { get; set; }
}