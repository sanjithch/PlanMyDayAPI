using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlanMyDayApp.Model.Uber;
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

        [HttpGet]
        [Route("getAddresses")]
        public async Task<string> GetAddresses()
        {
            //HttpClient httpClient = new HttpClient();

            Console.Write("Entered get address");

            //var url = "https://www.uber.com/api/loadTSSuggestions?localeCode=en";
            //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            //request.Content = new StringContent("{type=\"pickup\",q=\"Sa\",locale=\"en\",lat=\"38.9\",long=\"-77.04\"}", Encoding.UTF8, "application/json");
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
                //httpClient.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");
                //httpClient.DefaultRequestHeaders.Referrer = new Uri("https://www.uber.com/");
                //httpClient.DefaultRequestHeaders.Conte = new MediaTypeHeaderValue("application/json");
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
                string requestBody = "{\"type\":\"pickup\",\"q\":\"Sa\",\"locale\":\"en\",\"lat\":38.9,\"long\":-77.04}"; // Replace with your request body data


                // adding content to the request
                HttpContent requestContent = new StringContent(requestBody, null, "application/json");
                requestContent.Headers.ContentLength = requestBody.Length;
                //HttpContent content = new StringContent(requestBody);
                //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(url, requestContent);
                Console.Write(response);
                Console.Write(response.IsSuccessStatusCode);

                if (response.IsSuccessStatusCode)
                {
                    // Process the response here
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    //jsonResponse = "{" + jsonResponse + "}";
                    //var result = JsonConvert.DeserializeObject<UberAddressResponse>(jsonResponse);
                    //var json = JsonConvert.DeserializeObject(jsonResponse);
                    Console.WriteLine(jsonResponse);
                }
                else
                {
                    Console.WriteLine($"HTTP Error: {response.StatusCode}");
                }
            }


            //request.Content = new StringContent(JsonConvert.SerializeObject(new { type = "pickup", q = "Sa", locale = "en", lat = "38.9", Long = "-77.04" }), Encoding.UTF8, "application/json");
            //request.Content = new StringContent(JsonConvert.SerializeObject(new
            //{\"type\":\"pickup\",\"q\":\"Sa\",\"locale\":\"en\",\"lat\":38.9,\"long\":-77.04}), Encoding.UTF8, "application / json");
            //request.Headers.Add("Host", "www.uber.com");
            //request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/118.0");
            ////Mozilla / 5.0(Windows NT 10.0; Win64; x64; rv: 109.0) Gecko / 20100101 Firefox / 118.0
            //request.Headers.Add("Accept", "*/*");
            //request.Headers.Add("Accept-Language", "en-US,en;q=0.5");
            //request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            //request.Headers.Add("Referer", "https://www.uber.com/");
            //request.Headers.Add("x-csrf-token", "x");
            
            //request.Headers.Add("Origin", "https://www.uber.com");
            //request.Headers.Add("Alt-Used", "www.uber.com");
            //request.Headers.Add("Connection", "keep-alive");
            //request.Headers.Add("Cookie", "uber_sites_mayfly_marketing_visitor_id={\"session_id\":\"df58516c-446a-4f7b-a43c-be83300cea66\",\"session_time_ms\":1693021858350}; marketing_vistor_id=7b2fc193-2f87-4331-9f60-62dc06551335; uber_sites_geolocalization={%22best%22:{%22localeCode%22:%22en%22%2C%22countryCode%22:%22US%22%2C%22territoryId%22:8%2C%22territorySlug%22:%22washington-dc%22%2C%22territoryName%22:%22Washington%20D.C.%22}%2C%22url%22:{%22localeCode%22:%22%22}%2C%22user%22:{%22countryCode%22:%22US%22%2C%22territoryId%22:8%2C%22territoryGeoJson%22:[[{%22lat%22:39.466012%2C%22lng%22:-78.869276}%2C{%22lat%22:39.466012%2C%22lng%22:-76.140835}%2C{%22lat%22:37.510214%2C%22lng%22:-76.140835}%2C{%22lat%22:37.510214%2C%22lng%22:-78.869276}]]%2C%22territoryGeoPoint%22:{%22latitude%22:38.9%2C%22longitude%22:-77.04}%2C%22territorySlug%22:%22washington-dc%22%2C%22territoryName%22:%22Washington%20D.C.%22%2C%22localeCode%22:%22en%22}}; UBER_CONSENTMGR=1694021326064|consent:true; udi-id=EwxgbpsNPuhj/0Ms+R6RGBRjcq1RErUD321Q2mvjwmISQWHKJ3HyGzCx4YcZ0WXQmzUFw0i6L3hXvCn4DwFboWD9A7UWabcj6OaowvP80+lRgy5zp9vBOucrstgoUeqaUxu9GLOnNfTEXwYUrlv6nYXr77XTO4D7GsRhGaZh+/nW9IdYeruUKb+DQOmsHA5CRF/zBxsGRC7v8BLK4V6YeA==99cAcfddGFn64+AMvQprBg==3NppPCS8Z5V0hNktcFG2C/vTc1PjQb/iCWIyuk56qEM=; usl_rollout_id=b43a5835-aaed-4b4f-8c98-13635bd0450e; udi-fingerprint=Qen6/TSkEsCjaVpVCrD4VcJYhmzmOCxDdkVPVVoSvzJTNvYBNNcctCxp9qwWfPEW1aoxOOMlswcX68LLYZUu6A==VSd133/EzSekNnBGYY0jrWU1FkjrnxDvl23xWwJgZE4=; sid=QA.CAESEGtVD8OleEHfqO9S8KcljVcY1ZjEqAYiATEqJDAyNjNlZjg0LWVjY2YtNGI0YS04YjZmLWUyOGMxOTAyOTAwMjI81HG5m4bKLY81Ww2uOXCN0xLRDOh3IzTP_prxlkx7oK58Wg5NH-5ehIajBSsR7XpZkClBoWnOs8cVhtt1OgExQgh1YmVyLmNvbQ.RB-0mVNW1UCCv29xYiOW5O7WxBXbXRD8aLB0vDPoig8; csid=1.1695950293575.dYTpoMwywzMP4aNrykpsWKyvc8AHtsF1lOyHtFp9hMk=; _ua={\"session_id\":\"b4235b96-9ef0-4b6f-9a32-35be5d569ebb\",\"session_time_ms\":1693869595528}; uber_sites_mayfly_session_id={\"session_id\":\"c044b728-5c40-431a-8bea-4e79661ee731\",\"session_time_ms\":1694021307094}; jwt-session=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE2OTQwMjEzMDgsImV4cCI6MTY5NDEwNzcwOH0.6Aqbb2IrVfqFwnYgKg6lzsS_d4k9aXBRA6osub93y30; mp_adec770be288b16d9008c964acfba5c2_mixpanel=%7B%22distinct_id%22%3A%20%221e04b5ee-22a3-483f-824b-c27330615fae%22%2C%22%24device_id%22%3A%20%2218a2ff76ef31e-038f7cbf04a28b-d525429-1fa400-18a2ff76ef431e%22%2C%22%24search_engine%22%3A%20%22google%22%2C%22%24initial_referrer%22%3A%20%22https%3A%2F%2Fwww.google.com%2F%22%2C%22%24initial_referring_domain%22%3A%20%22www.google.com%22%2C%22%24user_id%22%3A%20%221e04b5ee-22a3-483f-824b-c27330615fae%22%7D");
            //request.Headers.Add("Sec-Fetch-Dest", "empty");
            //request.Headers.Add("Sec-Fetch-Mode", "cors");
            //request.Headers.Add("TE", "trailers");
            //request.Headers.Add("Sec-Fetch-Site", "same-origin");

            //request.Content.Headers.Add("Content-Length", "65");

            ////HttpClient httpClient = new HttpClient();
            ////httpClient.BaseAddress = new Uri(url);

            //var response = await httpClient.SendAsync(request);
            //Console.Write(response);

            return "Hii";
        }
    }
}
