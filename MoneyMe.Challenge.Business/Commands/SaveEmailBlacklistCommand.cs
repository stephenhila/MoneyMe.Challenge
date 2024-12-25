using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyMe.Challenge.Data;

namespace MoneyMe.Challenge.Business.Commands;

public class SaveEmailBlacklistCommandHandler : IRequestHandler<SaveEmailBlacklistCommand, Guid>
{
    private readonly ILoanContext _context;

    public SaveEmailBlacklistCommandHandler(ILoanContext context) => _context = context;

    public async Task<Guid> Handle(SaveEmailBlacklistCommand request, CancellationToken cancellationToken)
    {
        var existingEmailBlacklist = await _context.EmailBlacklist.FirstOrDefaultAsync(b => b.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase), cancellationToken);

        if (existingEmailBlacklist != default)
        {
            return existingEmailBlacklist.Id;
        }

        var newEmailBlacklist = new EmailBlacklist { Email = request.Email };
        _context.EmailBlacklist.Add(newEmailBlacklist);
        await _context.SaveChangesAsync(cancellationToken);
        return newEmailBlacklist.Id;
    }
}

public class SaveEmailBlacklistCommand : IRequest<Guid>
{
    public string Email { get; set; }
}
