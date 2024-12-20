
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentsForStudents.Context;
using System.Text;

namespace StudentsForStudents.Controllers
{
    public class PaymentController : Controller
    {
        private readonly SFSDBContect _context;

        private const string APILoginId = "7X4jbZ44";
        private const string TransactionKey = "7zWC9M93x2w2CZyw";

        public PaymentController(SFSDBContect context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();

        }

        public async Task<IActionResult> GetPaymentById(string id)
        {
            using (var client = new HttpClient())
            {
                string url = $"https://api.moyasar.com/v1/payments/{id}";
                string username = "sk_test_qWSsB4WEBstJp9eovR1j4HjwsjA1FnV1qWRmSYuW";
                string password = "";

                var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                var response = await client.GetStringAsync(url);

                // Return the response data as JSON
                return Content(response, "application/json");
            }
        }
        public async Task<IActionResult> GetAllPayment()
        {
            using (var client = new HttpClient())
            {
                string url = "https://api.moyasar.com/v1/payments";
                string username = "sk_test_qWSsB4WEBstJp9eovR1j4HjwsjA1FnV1qWRmSYuW";
                string password = "";

                var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                var response = await client.GetStringAsync(url);

                // Return the response data as JSON
                return Content(response, "application/json");
            }
        }

        public async Task<IActionResult> CancelPayment(string id)
        {
            using (var client = new HttpClient())
            {
                string url = $"https://api.moyasar.com/v1/payments/{id}/void";
                string username = "sk_test_qWSsB4WEBstJp9eovR1j4HjwsjA1FnV1qWRmSYuW";
                string password = "";

                var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                // Send the POST request
                var response = await client.PostAsync(url, null);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    var responseData = await response.Content.ReadAsStringAsync();

                    // Return the response data as JSON
                    return Content(responseData, "application/json");
                }
                else
                {
                    // Handle the case where the request was not successful
                    return BadRequest("Failed to cancel payment.");
                }
            }
        }
        public async Task<IActionResult> RefundPayment(string id)
        {
            var amountt = _context.Payment.FindAsync(id);
            using (var client = new HttpClient())
            {
                string url = $"https://api.moyasar.com/v1/payments/{id}/refund";
                string username = "sk_test_qWSsB4WEBstJp9eovR1j4HjwsjA1FnV1qWRmSYuW";
                string password = "";

                var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                // Specify the amount to be refunded in the request body
                var requestBody = new
                {
                    amount = amountt // Replace with the actual amount to be refunded
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                // Send the POST request with the request body
                var response = await client.PostAsync(url, content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    var responseData = await response.Content.ReadAsStringAsync();

                    // Return the response data as JSON
                    return Content(responseData, "application/json");
                }
                else
                {
                    // Handle the case where the request was not successful
                    return BadRequest("Failed to refund payment.");
                }
            }
        }
        public async Task<IActionResult> CapturePayment(string id)
        {
            var amountt = _context.Payment.FindAsync(id);
            using (var client = new HttpClient())
            {
                string url = $"https://api.moyasar.com/v1/payments/{id}/capture";
                string username = "sk_test_qWSsB4WEBstJp9eovR1j4HjwsjA1FnV1qWRmSYuW";
                string password = "";

                var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                // Specify the amount to be captured in the request body
                var requestBody = new
                {
                    amount = amountt // Replace with the actual amount to be captured
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                // Send the POST request with the request body
                var response = await client.PostAsync(url, content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    var responseData = await response.Content.ReadAsStringAsync();

                    // Return the response data as JSON
                    return Content(responseData, "application/json");
                }
                else
                {
                    // Handle the case where the request was not successful
                    return BadRequest("Failed to capture payment.");
                }
            }



        }
    }
}
