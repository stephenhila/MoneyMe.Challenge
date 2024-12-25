using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Data;

namespace MoneyMe.Challenge.Business.Queries;

public class GetMobileBlacklistByMobileNumberQueryHandler : IRequestHandler<GetMobileBlacklistByMobileNumberQuery, MobileNumberBlacklistDTO>
{
    private readonly LoanContext _context;
    private readonly IMapper _mapper;

    public GetMobileBlacklistByMobileNumberQueryHandler(LoanContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<MobileNumberBlacklistDTO> Handle(GetMobileBlacklistByMobileNumberQuery request, CancellationToken cancellationToken)
    {
        var mobileBlacklist = await _context.MobileNumberBlacklist.FirstOrDefaultAsync(mobile => mobile.Mobile == request.MobileNumber, cancellationToken);

        return _mapper.Map<MobileNumberBlacklistDTO>(mobileBlacklist);
    }
}

public class GetMobileBlacklistByMobileNumberQuery : IRequest<MobileNumberBlacklistDTO>
{
    public string MobileNumber { get; set; }
}
