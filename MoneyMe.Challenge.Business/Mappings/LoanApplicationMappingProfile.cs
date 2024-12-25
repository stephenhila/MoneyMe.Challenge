using AutoMapper;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Data;

namespace MoneyMe.Challenge.Business.Mappings;

public class LoanApplicationMappingProfile : Profile
{
    public LoanApplicationMappingProfile()
    {
        CreateMap<LoanApplication, LoanApplicationDTO>();
        CreateMap<LoanApplicationDTO, LoanApplication>();

        CreateMap<EmailBlacklist, EmailBlacklistDTO>();
        CreateMap<EmailBlacklistDTO, EmailBlacklist>();

        CreateMap<MobileNumberBlacklist, MobileNumberBlacklistDTO>();
        CreateMap<MobileNumberBlacklistDTO, MobileNumberBlacklist>();
    }
}
