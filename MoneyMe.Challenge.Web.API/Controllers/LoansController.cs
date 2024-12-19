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
    private readonly IMediator _mediator;

    public LoansController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Apply([FromBody] LoanApplicationDTO request)
    {
        Guid loanApplicationId = await _mediator.Send(new SaveLoanApplicationCommand { LoanApplication = request });

        return Ok(loanApplicationId);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        LoanApplicationDTO loanApplication = await _mediator.Send(new GetLoanApplicationQuery { Id = id });

        return Ok(loanApplication);
    }
}
