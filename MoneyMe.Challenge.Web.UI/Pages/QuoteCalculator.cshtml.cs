using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneyMe.Challenge.Business.DTO;
using Newtonsoft.Json;
using System.Text;

namespace MoneyMe.Challenge.Web.UI.Pages
{
    public class QuoteCalculatorModel : PageModel
    {
        [BindProperty]
        public LoanApplicationDTO LoanApplication { get; set; }

        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }

        private readonly ILogger<QuoteCalculatorModel> _logger;
        private readonly IConfiguration _configuration;

        public QuoteCalculatorModel(ILogger<QuoteCalculatorModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            // Create an HttpClient instance
            using (var httpClient = new HttpClient())
            {
                // Construct the API URL
                var apiUrl = $"{_configuration["ApiBaseUrl"]}/loans/{id}"; // Replace with your API URL

                // Serialize the request body to JSON
                var jsonContent = new StringContent(JsonConvert.SerializeObject(LoanApplication), Encoding.UTF8, "application/json");

                // Send the POST request
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response (assuming it contains the redirect URL)
                    var responseContent = await response.Content.ReadAsStringAsync();
                    LoanApplication = JsonConvert.DeserializeObject<LoanApplicationDTO>(responseContent);

                    MinValue = Math.Min(2100, LoanApplication.AmountRequired);
                    MaxValue = Math.Max(10000, LoanApplication.AmountRequired);
                }
                else
                {
                    // Handle error
                    return BadRequest();
                }
            }

            if (LoanApplication == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
