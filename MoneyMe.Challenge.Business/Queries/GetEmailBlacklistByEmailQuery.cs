using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Data;

namespace MoneyMe.Challenge.Business.Queries;

public class GetEmailBlacklistByEmailQueryHandler : IRequestHandler<GetEmailBlacklistByEmailQuery, EmailBlacklistDTO>
{
    private readonly LoanContext _context;
    private readonly IMapper _mapper;

    public GetEmailBlacklistByEmailQueryHandler(LoanContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EmailBlacklistDTO> Handle(GetEmailBlacklistByEmailQuery request, CancellationToken cancellationToken)
    {
        var emailBlacklist = await _context.EmailBlacklist.FirstOrDefaultAsync(email => email.Email == request.Email, cancellationToken);

        return _mapper.Map<EmailBlacklistDTO>(emailBlacklist);
    }
}

public class GetEmailBlacklistByEmailQuery : IRequest<EmailBlacklistDTO>
{
    public string Email { get; set; }
}
