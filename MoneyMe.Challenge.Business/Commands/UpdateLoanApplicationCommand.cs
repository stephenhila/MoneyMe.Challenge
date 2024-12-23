using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Data;

namespace MoneyMe.Challenge.Business.Commands;

public class UpdateLoanApplicationCommandHandler : IRequestHandler<UpdateLoanApplicationCommand, bool>
{
    private readonly ILoanContext _context;
    private readonly IMapper _mapper;

    public UpdateLoanApplicationCommandHandler(ILoanContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

    public async Task<bool> Handle(UpdateLoanApplicationCommand request, CancellationToken cancellationToken)
    {
        Guid.TryParse(request.Id, out var originalLoanApplicationId);
        var originalLoanApplication = await _context.LoanApplications.FirstOrDefaultAsync(e => e.Id == originalLoanApplicationId);
        
        if (originalLoanApplication == default)
        {
            return false;
        }

        originalLoanApplication.AmountRequired = request.LoanApplication.AmountRequired;
        originalLoanApplication.Term = request.LoanApplication.Term;
        originalLoanApplication.Title = request.LoanApplication.Title;
        originalLoanApplication.FirstName = request.LoanApplication.FirstName;
        originalLoanApplication.LastName = request.LoanApplication.LastName;
        originalLoanApplication.DateOfBirth = request.LoanApplication.DateOfBirth;
        originalLoanApplication.Mobile = request.LoanApplication.Mobile;
        originalLoanApplication.Email = request.LoanApplication.Email;

        _context.Entry(originalLoanApplication).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return true;
    }
}

public class UpdateLoanApplicationCommand : IRequest<bool>
{
    public string Id { get; set; }
    public LoanApplicationDTO LoanApplication { get; set; }
}