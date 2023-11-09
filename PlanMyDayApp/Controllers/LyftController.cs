using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanMyDayApp.Model.Uber;
using PlanMyDayApp.Model.Uber.GettingAddress;

namespace PlanMyDayApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LyftController : ControllerBase
    {

        HelperforUberRequests helper = new HelperforUberRequests();

        [HttpPost]
        [Route("getFaresFromLyft")]
        public async Task<IActionResult> GetFareFromLyft(JourneyCoOrdinates journeyCoOrdinates)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // General Headers
                //httpClient.DefaultRequestHeaders.Date = DateTimeOffset.UtcNow;

                // Request Headers
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/118.0");
                httpClient.DefaultRequestHeaders.Host = "ride.lyft.com";
                httpClient.DefaultRequestHeaders.Add("Referer", "https://ride.lyft.com/ridetype?origin=38.877548%2C-94.609733&destination=39.295503%2C-94.720197");

                // Representation Headers (Request)
                //httpClient.DefaultRequestHeaders.Add("Accept-Encoding", " gzip, deflate, br");
                httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.5");
                //httpClient.DefaultRequestHeaders.Add("x-csrf-token", "x");
                httpClient.DefaultRequestHeaders.Add("TE", " trailers");

                // Other Headers
                //httpClient.DefaultRequestHeaders.Add("Content-Length", "67");
                httpClient.DefaultRequestHeaders.Add("x-locale-language", "en-US");
                httpClient.DefaultRequestHeaders.Add("x-device-density", "1");
                httpClient.DefaultRequestHeaders.Add("Origin", "https://ride.lyft.com");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
                //httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
                httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");

                //authentication key
                //httpClient.DefaultRequestHeaders.Add("Cookie", "sessId=b02c56a2-cdc5-4487-a963-a7f53040ed34L1694794041; OptanonConsent=isGpcEnabled=0&datestamp=Fri+Sep+15+2023+11%3A09%3A07+GMT-0500+(Central+Daylight+Time)&version=202211.2.0&isIABGlobal=false&hosts=&consentId=79e5a125-4db1-4d83-ae60-702872951909&interactionCount=1&landingPath=NotLandingPage&groups=C0001%3A1%2CC0003%3A1%2CC0002%3A1%2CC0004%3A1&AwaitingReconsent=false; stickyLyftBrowserId=2hx324xswz45mg76ph0Uja5E; lyftAccessToken=52qSjzkHYNO4T9BKe65nYHqg9qreewJ1kRUavT5EUgSUwybRIUcHNTqfPq+2CAqfCimfciYGEEO8X3j+CAazSyVY0j2Awf5th9B78/SxraFFEaH8aCwK+7Y=; __stripe_mid=6ee5da35-fd76-4acc-a35f-90733e53053e2eb472; __stripe_sid=bf23b406-1bb1-4559-bd6e-23274c99d16de5ef88");
                httpClient.DefaultRequestHeaders.Add("Cookie", helper.GetConfiguration("LyftSession"));
                // Make an HTTP POST request to a URL
                string url = "https://ride.lyft.com/v1/offerings";
                string requestBody = helper.GiveBodyForGettingFaresFromLyft(journeyCoOrdinates);


                // adding content to the request
                HttpContent requestContent = new StringContent(requestBody, null, "application/json");
                requestContent.Headers.ContentLength = requestBody.Length;
                HttpResponseMessage response = await httpClient.PostAsync(url, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Got Fares........... from Lyft");
                    Console.WriteLine(jsonResponse);
                    Console.WriteLine("Got Fares............ from Lyft");

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
    }
}
