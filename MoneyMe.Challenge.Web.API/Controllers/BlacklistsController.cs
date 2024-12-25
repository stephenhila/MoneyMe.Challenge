using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyMe.Challenge.Business.Commands;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Business.Queries;

namespace MoneyMe.Challenge.Web.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlacklistsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BlacklistsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("email")]
    public async Task<IActionResult> AddEmailBlacklist([FromBody] string email)
    {
        await _mediator.Send(new SaveEmailBlacklistCommand { Email = email });
        return Ok();
    }

    [HttpGet("email")]
    public async Task<IActionResult> GetEmailBlacklist([FromQuery] string value)
    {
        EmailBlacklistDTO emailBlacklist = await _mediator.Send(new GetEmailBlacklistByEmailQuery { Email = value });

        if (emailBlacklist != null)
            return Ok(emailBlacklist);
        else
            return NotFound();
    }

    [HttpPost("mobile")]
    public async Task<IActionResult> AddMobileBlacklist([FromBody] string mobile)
    {
        await _mediator.Send(new SaveMobileNumberBlacklistCommand { MobileNumber = mobile });
        return Ok();
    }

    [HttpGet("mobile")]
    public async Task<IActionResult> GetMobileBlacklist([FromQuery] string value)
    {
        MobileNumberBlacklistDTO mobileBlacklist = await _mediator.Send(new GetMobileBlacklistByMobileNumberQuery { MobileNumber = value });

        if (mobileBlacklist != null)
            return Ok(mobileBlacklist);
        else
            return NotFound();
    }

}
