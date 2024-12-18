using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyMe.Challenge.Business.Commands;
using MoneyMe.Challenge.Business.DTO;

namespace MoneyMe.Challenge.Web.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoansController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoansController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CalculateLoanPayment([FromBody] LoanApplicationDTO request)
    {
        Guid loanApplicationId = await _mediator.Send(new SaveLoanApplicationCommand { LoanApplication = request });

        return Ok(loanApplicationId);
    }
}
