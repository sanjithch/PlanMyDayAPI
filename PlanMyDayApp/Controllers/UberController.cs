using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlanMyDayApp.Model.Uber;
using PlanMyDayApp.Model.Uber.GettingAddress;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlanMyDayApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UberController : ControllerBase
    {
        HelperforUberRequests helper = new HelperforUberRequests();

        // GET: api/<UberController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UberController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //To get Addresses from Uber application
        [HttpGet]
        [Route("getAddresses/{id}")]
        public async Task<IEnumerable<string>> GetAddresses(string id)
        {
            //HttpClient httpClient = new HttpClient();

            Console.Write("Searching for address - " + id);
            String[] arr = new string[2];

            using (HttpClient httpClient = new HttpClient())
            {
                // General Headers
                httpClient.DefaultRequestHeaders.Date = DateTimeOffset.UtcNow;

                // Request Headers
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/118.0");
                httpClient.DefaultRequestHeaders.Host = "www.uber.com";

                // Representation Headers (Request)
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.5");
                httpClient.DefaultRequestHeaders.Add("x-csrf-token", "x");

                // Other Headers
                //httpClient.DefaultRequestHeaders.Add("Content-Length", "67");
                httpClient.DefaultRequestHeaders.Add("Origin", "https://www.uber.com");
                httpClient.DefaultRequestHeaders.Add("Alt-Used", "www.uber.com");
                httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
                httpClient.DefaultRequestHeaders.Add("Cookie", "Add your cookie values here");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
                httpClient.DefaultRequestHeaders.Add("TE", "trailers");

                // Make an HTTP POST request to a URL
                string url = "https://www.uber.com/api/loadTSSuggestions?localeCode=en";
                string requestBody = helper.GiveBodyForLocationRequest(id);


                // adding content to the request
                HttpContent requestContent = new StringContent(requestBody, null, "application/json");
                requestContent.Headers.ContentLength = requestBody.Length;
                HttpResponseMessage response = await httpClient.PostAsync(url, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    AddressResponseModel address = JsonConvert.DeserializeObject<AddressResponseModel>(jsonResponse);
                    arr = new string[address.data.candidates.Count];

                    for(int i=0;i<address.data.candidates.Count;i++)
                    {
                        arr[i] = address.data.candidates[i].addressLine1+ address.data.candidates[i].addressLine2;
                        Console.WriteLine(arr[i]);
                    }
                }
                else
                {
                    Console.WriteLine($"HTTP Error: {response.StatusCode}");
                }
            }

            return arr;
        }



        // To get the price between two locations
        [HttpGet]
        [Route("getRides/{from}&{to}")]
        public async Task<IEnumerable<string>> GetRides(string from, string to)
        {

            return null;
        }
    }
}
