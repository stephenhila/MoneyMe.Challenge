using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyMe.Challenge.Business.Commands;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Business.Queries;

namespace MoneyMe.Challenge.Web.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoansController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;

    public LoansController(IConfiguration configuration, IMediator mediator) => (_configuration, _mediator) = (configuration, mediator);

    [HttpPost]
    public async Task<IActionResult> Apply([FromBody] LoanApplicationDTO request)
    {
        Guid loanApplicationId = await _mediator.Send(new SaveLoanApplicationCommand { LoanApplication = request });

        var redirectUrl = $"{_configuration["FrontEndBaseUrl"]}/loans/{loanApplicationId}";
        return Ok(new { RedirectUrl = redirectUrl});
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        LoanApplicationDTO loanApplication = await _mediator.Send(new GetLoanApplicationQuery { Id = id });

        return Ok(loanApplication);
    }
}
