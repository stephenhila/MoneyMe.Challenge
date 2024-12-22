using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneyMe.Challenge.Business.DTO;
using Newtonsoft.Json;
using System.Text;

namespace MoneyMe.Challenge.Web.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public LoanApplicationDTO LoanApplication { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Handle validation errors
                return Page();
            }

            // Create an HttpClient instance
            using (var httpClient = new HttpClient())
            {
                // Construct the API URL
                var apiUrl = $"{_configuration["ApiBaseUrl"]}/loans"; // Replace with your API URL

                // Serialize the request body to JSON
                var jsonContent = new StringContent(JsonConvert.SerializeObject(LoanApplication), Encoding.UTF8, "application/json");

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

        #region Fields
        public decimal AmountRequired { get; set; }
        public int Term { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        #endregion
    }
}
