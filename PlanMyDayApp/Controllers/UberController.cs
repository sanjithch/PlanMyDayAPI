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
        UberSession session = new UberSession();
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
        public async Task<IEnumerable<ResponseWithIDandAddress>> GetAddresses(string id)
        {
            //HttpClient httpClient = new HttpClient();
            AddressResponseModel address;

            Console.Write("Searching for address - " + id);
            ResponseWithIDandAddress[] arr = new ResponseWithIDandAddress[2];

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
                    address = new AddressResponseModel();
                    address = JsonConvert.DeserializeObject<AddressResponseModel>(jsonResponse);
                    arr = new ResponseWithIDandAddress[address.data.candidates.Count];

                    for (int i = 0; i < address.data.candidates.Count; i++)
                    {

                        arr[i] = new ResponseWithIDandAddress(address.data.candidates[i].id, address.data.candidates[i].addressLine1 + address.data.candidates[i].addressLine2);
                        Console.WriteLine(JsonConvert.SerializeObject(arr[i]));
                    }
                }
                else
                {
                    Console.WriteLine($"HTTP Error: {response.StatusCode}");
                }
            }
            //Console.WriteLine(JsonConvert.SerializeObject(session));
            return arr;
        }



        //once the address is selected to get co-ordinates
        [HttpGet]
        [Route("selectingAdress/{id}")]
        public async Task<IActionResult> SelectingAddress(string id)
        {
            Location arr = new Location();
            ResponseForCooridnatesForAddress address;
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
                //httpClient.DefaultRequestHeaders.Add("Cookie", "Add your cookie values here");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
                httpClient.DefaultRequestHeaders.Add("TE", "trailers");

                // Make an HTTP POST request to a URL
                string url = "https://www.uber.com/api/loadTSPlaceDetails?localeCode=en";
                string requestBody = helper.GiveBodyforLocationCoordinates(id);


                // adding content to the request
                HttpContent requestContent = new StringContent(requestBody, null, "application/json");
                requestContent.Headers.ContentLength = requestBody.Length;
                HttpResponseMessage response = await httpClient.PostAsync(url, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Success");
                    Console.WriteLine(jsonResponse);
                    Console.WriteLine("Success");

                    return Ok(jsonResponse);
                }
                else
                {
                    Console.WriteLine($"HTTP Error: {response.StatusCode}");
                }
            }
            Console.WriteLine(JsonConvert.SerializeObject(arr));
            //helper.MappingSelectedAddress(session, type, selectAddress);
            //Console.WriteLine(session);
            return Ok();
        }

        //for getting fares from uber;
        [HttpPost]
        [Route("getFaresFromUber")]
        public async Task<IActionResult> GetFarePrices(JourneyCoOrdinates journeyCoOrdinates)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // General Headers
                httpClient.DefaultRequestHeaders.Date = DateTimeOffset.UtcNow;

                // Request Headers
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/118.0");
                httpClient.DefaultRequestHeaders.Host = "m.uber.com";
                httpClient.DefaultRequestHeaders.Add("Referer", "https://m.uber.com/looking?drop%5B0%5D=%7B%22id%22%3A%22c5f5bb9f-585e-469d-8274-de18a8451f34%22%2C%22provider%22%3A%22uber_places%22%2C%22addressLine1%22%3A%22KC%20India%20Mart%22%2C%22addressLine2%22%3A%228542%20W%20133rd%20St%2C%20Overland%20Park%2C%20KS%22%2C%22latitude%22%3A38.8871021%2C%22longitude%22%3A-94.6849515%2C%22index%22%3A0%7D&pickup=%7B%22id%22%3A%22ffc81172-d473-4d03-2c23-9275d488d5ad%22%2C%22provider%22%3A%22uber_places%22%2C%22addressLine1%22%3A%226655%20W%20141st%20St%22%2C%22addressLine2%22%3A%22Overland%20Park%2C%20KS%22%2C%22latitude%22%3A38.873754%2C%22longitude%22%3A-94.664654%2C%22index%22%3A0%7D");

                // Representation Headers (Request)
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.5");
                httpClient.DefaultRequestHeaders.Add("x-csrf-token", "x");

                // Other Headers
                //httpClient.DefaultRequestHeaders.Add("Content-Length", "67");
                httpClient.DefaultRequestHeaders.Add("Origin", " https://m.uber.com");
                httpClient.DefaultRequestHeaders.Add("Alt-Used", "www.uber.com");
                httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
                //httpClient.DefaultRequestHeaders.Add("Cookie", "Add your cookie values here");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
                httpClient.DefaultRequestHeaders.Add("TE", "trailers");
                httpClient.DefaultRequestHeaders.Add("x-uber-rv-session-type", " desktop_session");
                httpClient.DefaultRequestHeaders.Add("x-uber-wa-info", " IHJFRSRRQRIKQTJGKHPNSRHI");
                httpClient.DefaultRequestHeaders.Add("Cookie", " marketing_vistor_id=7b2fc193-2f87-4331-9f60-62dc06551335; UBER_CONSENTMGR=1694543244351|consent:true; udi-id=EwxgbpsNPuhj/0Ms+R6RGBRjcq1RErUD321Q2mvjwmISQWHKJ3HyGzCx4YcZ0WXQmzUFw0i6L3hXvCn4DwFboWD9A7UWabcj6OaowvP80+lRgy5zp9vBOucrstgoUeqaUxu9GLOnNfTEXwYUrlv6nYXr77XTO4D7GsRhGaZh+/nW9IdYeruUKb+DQOmsHA5CRF/zBxsGRC7v8BLK4V6YeA==99cAcfddGFn64+AMvQprBg==3NppPCS8Z5V0hNktcFG2C/vTc1PjQb/iCWIyuk56qEM=; usl_rollout_id=b43a5835-aaed-4b4f-8c98-13635bd0450e; udi-fingerprint=zSDj63pEDCysay7nLZ9SKqj6A+NFKxwCMhGdOTxrjuiWP/MAXxZHl9S48k/dwvsHCsM8c9mv4xX9WAuRW+jqvg==xOcAvb7JITrechca1v77U0IeTDHPmZKg/jypkF9fOJ8=; sid=QA.CAESEGtVD8OleEHfqO9S8KcljVcY1ZjEqAYiATEqJDAyNjNlZjg0LWVjY2YtNGI0YS04YjZmLWUyOGMxOTAyOTAwMjI81HG5m4bKLY81Ww2uOXCN0xLRDOh3IzTP_prxlkx7oK58Wg5NH-5ehIajBSsR7XpZkClBoWnOs8cVhtt1OgExQgh1YmVyLmNvbQ.RB-0mVNW1UCCv29xYiOW5O7WxBXbXRD8aLB0vDPoig8; csid=1.1695616085309.seIoMuhRPYWPtTtARnjEmVTtuwDfZqpAkq9FXr20vZs=; _cc=ActnoDLHXttro17Qyl0vzj%2Bm; _cid_cc=ActnoDLHXttro17Qyl0vzj%2Bm; _ua={\"session_id\":\"a589d976-b6c3-463e-b794-cf8401e44ac2\",\"session_time_ms\":1694543282004}; jwt-session=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJkYXRhIjp7InNlc3Npb25fdHlwZSI6ImRlc2t0b3Bfc2Vzc2lvbiIsInRlbmFuY3kiOiJ1YmVyL3Byb2R1Y3Rpb24ifSwiaWF0IjoxNjk0NTQzMjgyLCJleHAiOjE2OTQ2Mjk2ODJ9.vXPoXCCIF9FFFyCgcf2WbYSoM-iuFDTtUx0dintVAJk; mp_adec770be288b16d9008c964acfba5c2_mixpanel=%7B%22distinct_id%22%3A%20%220263ef84-eccf-4b4a-8b6f-e28c19029002%22%2C%22%24device_id%22%3A%20%2218a2ff76ef31e-038f7cbf04a28b-d525429-1fa400-18a2ff76ef431e%22%2C%22%24search_engine%22%3A%20%22google%22%2C%22%24initial_referrer%22%3A%20%22https%3A%2F%2Fwww.google.com%2F%22%2C%22%24initial_referring_domain%22%3A%20%22www.google.com%22%2C%22%24user_id%22%3A%20%220263ef84-eccf-4b4a-8b6f-e28c19029002%22%7D; marketing_vistor_id=7b2fc193-2f87-4331-9f60-62dc06551335; _ua={\"session_id\":\"a589d976-b6c3-463e-b794-cf8401e44ac2\",\"session_time_ms\":1694543282004}; jwt-session=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE2OTQ3MTY3NTIsImV4cCI6MTY5NDgwMzE1Mn0.9ua40PGlTuH5kz7T4dGtvRmwNoutR1VR-ftdoSItexs");

                // Make an HTTP POST request to a URL
                string url = "https://m.uber.com/graphql";
                string requestBody = helper.GiveBodyforGettingFares(journeyCoOrdinates);


                // adding content to the request
                HttpContent requestContent = new StringContent(requestBody, null, "application/json");
                requestContent.Headers.ContentLength = requestBody.Length;
                HttpResponseMessage response = await httpClient.PostAsync(url, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Got Fares...........");
                    Console.WriteLine(jsonResponse);
                    Console.WriteLine("Got Fares............");

                    return Ok(jsonResponse);
                }
                else
                {
                    Console.WriteLine($"HTTP Error: {response.StatusCode}");
                }
            }
            //helper.MappingSelectedAddress(session, type, selectAddress);
            //Console.WriteLine(session);
            return Ok();
        }




        //    // To get the price between two locations
        //    [HttpGet]
        //    [Route("getRides/{from}&{to}")]
        //    public async Task<IEnumerable<string>> GetRides(string from, string to)
        //    {

        //        return null;
        //    }
        //}
    }
}
