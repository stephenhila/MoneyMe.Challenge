using MoneyMe.Challenge.Business.DTO;
using Refit;

namespace MoneyMe.Challenge.Web.UI.Services;

public interface IBlacklistService
{
    [Get("/blacklists/email")]
    Task<EmailBlacklistDTO> GetEmailBlacklistAsync([Body]string email);

    //[Post("/blacklists/email")]
    //Task<string> AddEmailBlacklistAsync([Body] string email);

    [Get("/blacklists/mobile")]
    Task<EmailBlacklistDTO> GetMobileNumberBlacklistAsync([Body] string mobileNumber);

    //[Post("/blacklists/mobile")]
    //Task<string> AddMobileNumberBlacklistAsync([Body] string mobileNumber);
}
