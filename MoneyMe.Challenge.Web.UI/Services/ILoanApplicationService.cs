using MoneyMe.Challenge.Business.DTO;
using Refit;

namespace MoneyMe.Challenge.Web.UI.Services;

public interface ILoanApplicationService
{
    [Get("/loans/{id}")]
    Task<LoanApplicationDTO> GetLoanApplicationAsync(Guid id);

    [Post("/loans")]
    Task<string> SubmitAndGetLoanApplicationRedirectUrlAsync([Body] LoanApplicationDTO loanApplication);

    [Put("/loans")]
    Task<bool> UpdateLoanApplicationAsync([Body] LoanApplicationDTO loanApplication);
}
