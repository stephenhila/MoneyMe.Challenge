using AutoMapper;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Data;

namespace MoneyMe.Challenge.Business.Mappings;

public class LoanApplicationMappingProfile : Profile
{
    public LoanApplicationMappingProfile()
    {
        CreateMap<LoanApplication, LoanApplicationDTO>();
    }
}
