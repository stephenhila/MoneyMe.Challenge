using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyMe.Challenge.Business.DTO;
using MoneyMe.Challenge.Business.Queries;
using MoneyMe.Challenge.Web.UI.Pages;
using Newtonsoft.Json;
using System.Text;

namespace MoneyMe.Challenge.Web.UI.Controllers;

[Route("[controller]")]
public class LoansController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;

    public LoansController(IConfiguration configuration, IMediator mediator) => (_configuration, _mediator) = (configuration, mediator);

    [HttpPost]
    public async Task<IActionResult> PostLoanApplication(LoanApplicationDTO request)
    {
        // Create an HttpClient instance
        using (var httpClient = new HttpClient())
        {
            // Construct the API URL
            var apiUrl = $"{_configuration["ApiBaseUrl"]}/loans"; // Replace with your API URL

            // Serialize the request body to JSON
            var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Send the POST request
            var response = await httpClient.PostAsync(apiUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                // Parse the response (assuming it contains the redirect URL)
                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

                var redirectUrl = Convert.ToString(apiResponse.redirectUrl);

                return Redirect(redirectUrl);
            }
            else
            {
                // Handle error
                return BadRequest();
            }
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> LoanApplicationDetails(Guid id)
    {
        LoanApplicationDTO loanApplication = await _mediator.Send(new GetLoanApplicationQuery { Id = id.ToString() });

        return View(loanApplication);
    }
}
