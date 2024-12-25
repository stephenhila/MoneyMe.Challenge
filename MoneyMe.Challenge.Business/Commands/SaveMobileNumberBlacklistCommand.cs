using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyMe.Challenge.Data;

namespace MoneyMe.Challenge.Business.Commands;

public class SaveMobileNumberBlacklistCommandHandler : IRequestHandler<SaveMobileNumberBlacklistCommand, Guid>
{
    private readonly ILoanContext _context;

    public SaveMobileNumberBlacklistCommandHandler(ILoanContext context) => _context = context;

    public async Task<Guid> Handle(SaveMobileNumberBlacklistCommand request, CancellationToken cancellationToken)
    {
        var existingMobileNumberBlacklist = await _context.MobileNumberBlacklist.FirstOrDefaultAsync(b => b.Mobile.Equals(request.MobileNumber, StringComparison.OrdinalIgnoreCase), cancellationToken);

        if (existingMobileNumberBlacklist != default)
        {
            return existingMobileNumberBlacklist.Id;
        }

        var newMobileNumberBlacklist = new MobileNumberBlacklist { Mobile = request.MobileNumber };
        _context.MobileNumberBlacklist.Add(newMobileNumberBlacklist);
        await _context.SaveChangesAsync(cancellationToken);
        return newMobileNumberBlacklist.Id;
    }
}

public class SaveMobileNumberBlacklistCommand : IRequest<Guid>
{
    public string MobileNumber {  get; set; }
}
