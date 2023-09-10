using System.Net.Http.Headers;
using System.Net.Http;

namespace PlanMyDayApp.Model.Uber
{
    public class HelperforUberRequests
    {
        // Common template for requesting body for a uber address search request;
        public string GiveBodyForLocationRequest(string body)
        {
            string result = "{\"type\":\"pickup\",\"q\":\"" + body + "\",\"locale\":\"en\",\"lat\":38.9,\"long\":-77.04}";
            Console.WriteLine(result);

            return result;
        }

        //for future reference
        public HttpClient GetHttpClient() {

            //using key word is used for resource management that ensures that disposable objects are properly cleaned up
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Date = DateTimeOffset.UtcNow;

                // Request Headers
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/118.0");
                httpClient.DefaultRequestHeaders.Host = "www.uber.com";

                // Representation Headers (Request)
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.5");
                //httpClient.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");
                httpClient.DefaultRequestHeaders.Add("x-csrf-token", "x");

                // Other Headers
                httpClient.DefaultRequestHeaders.Add("Origin", "https://www.uber.com");
                httpClient.DefaultRequestHeaders.Add("Alt-Used", "www.uber.com");
                httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
                httpClient.DefaultRequestHeaders.Add("Cookie", "Add your cookie values here");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
                httpClient.DefaultRequestHeaders.Add("TE", "trailers");

                return httpClient;
            }
            return new HttpClient();
        }
    }
}
