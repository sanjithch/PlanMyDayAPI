using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using PlanMyDayApp.Model.Uber.GettingAddress;
using Newtonsoft.Json;

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

        // getting body for co-ordinated request for id of address
        public string GiveBodyforLocationCoordinates(string body)
        {
            string result = "{\"type\":\"pickup\",\"locale\":\"en\",\"id\":\"" + body + "\",\"provider\":\"uber_places\"}";
            Console.WriteLine(result);
            return result;
        }

        //give body for getting fares from one location to other
        public string GiveBodyforGettingFares(JourneyCoOrdinates journeyCoOrdinates)
        {
            //[{\"latitude\":38.8871021,\"longitude\":-94.6849515}],\"pickup\":{\"latitude\":38.873754,\"longitude\":-94.664654}
            string s = JsonConvert.SerializeObject(journeyCoOrdinates);
            s = s.Substring(1, s.Length - 2);
            string result = "{\"operationName\":\"Products\",\"variables\":{\"includeClassificationFilters\":false,\"includeRecommended\":false," + s + ",\"targetProductType\":null},\"query\":\"query Products($destinations: [InputCoordinate!]!, $includeClassificationFilters: Boolean = false, $includeRecommended: Boolean = false, $pickup: InputCoordinate!, $pickupFormattedTime: String, $profileType: String, $targetProductType: EnumRVWebCommonTargetProductType) {\\n  products(\\n    destinations: $destinations\\n    includeClassificationFilters: $includeClassificationFilters\\n    includeRecommended: $includeRecommended\\n    pickup: $pickup\\n    pickupFormattedTime: $pickupFormattedTime\\n    profileType: $profileType\\n    targetProductType: $targetProductType\\n  ) {\\n    ...ProductsFragment\\n    __typename\\n  }\\n}\\n\\nfragment ProductsFragment on RVWebCommonProductsResponse {\\n  classificationFilters {\\n    ...ClassificationFiltersFragment\\n    __typename\\n  }\\n  defaultVVID\\n  productsUnavailableMessage\\n  renderRankingInformation\\n  tiers {\\n    ...TierFragment\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment BadgesFragment on RVWebCommonProductBadge {\\n  color\\n  text\\n  __typename\\n}\\n\\nfragment ClassificationFiltersFragment on RVWebCommonClassificationFilters {\\n  filters {\\n    ...ClassificationFilterFragment\\n    __typename\\n  }\\n  hiddenVVIDs\\n  standardProductVVID\\n  __typename\\n}\\n\\nfragment ClassificationFilterFragment on RVWebCommonClassificationFilter {\\n  currencyCode\\n  displayText\\n  fareDifference\\n  icon\\n  vvid\\n  __typename\\n}\\n\\nfragment TierFragment on RVWebCommonProductTier {\\n  products {\\n    ...ProductFragment\\n    __typename\\n  }\\n  title\\n  __typename\\n}\\n\\nfragment ProductFragment on RVWebCommonProduct {\\n  badges {\\n    ...BadgesFragment\\n    __typename\\n  }\\n  capacity\\n  cityID\\n  currencyCode\\n  description\\n  detailedDescription\\n  discountPrimary\\n  displayName\\n  estimatedTripTime\\n  etaStringShort\\n  fare\\n  hasPromo\\n  hasRidePass\\n  id\\n  is3p\\n  isAvailable\\n  legalConsent {\\n    ...ProductLegalConsentFragment\\n    __typename\\n  }\\n  meta\\n  preAdjustmentValue\\n  productImageUrl\\n  productUuid\\n  reserveEnabled\\n  __typename\\n}\\n\\nfragment ProductLegalConsentFragment on RVWebCommonProductLegalConsent {\\n  header\\n  image {\\n    url\\n    width\\n    __typename\\n  }\\n  description\\n  enabled\\n  ctaUrl\\n  ctaDisplayString\\n  buttonLabel\\n  showOnce\\n  __typename\\n}\\n\"}";
            Console.WriteLine(result);
            return result;
        }

        //mapping selected address into the session object
        public void MappingSelectedAddress(UberSession session, string type, string address)
        {
            if (type == "to")
            {
                foreach (var a in session.addressResponseModelforTo.data.candidates)
                {
                    if (a.addressLine1 + a.addressLine2 == address)
                    {
                        session.journeyModel.From.addressLine1 = a.addressLine1;
                        session.journeyModel.From.addressLine2 = a.addressLine2;
                        session.journeyModel.From.id = a.id;
                    }
                }
            }
            else
            {
                foreach (var a in session.addressResponseModelforFrom.data.candidates)
                {
                    if (a.addressLine1 + a.addressLine2 == address)
                    {
                        session.journeyModel.To.addressLine1 = a.addressLine1;
                        session.journeyModel.To.addressLine2 = a.addressLine2;
                        session.journeyModel.To.id = a.id;
                    }
                }
            }
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
            //return new HttpClient();
        }
    }
}
