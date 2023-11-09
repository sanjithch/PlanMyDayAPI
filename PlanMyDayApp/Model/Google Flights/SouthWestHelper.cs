using PlanMyDayApp.Model.Uber.GettingAddress;

namespace PlanMyDayApp.Model.Google_Flights
{
    public class SouthWestHelper
    {
        public string GetConfiguration(string requestFor)
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json").Build();
            //Console.WriteLine("........................");
            //Console.WriteLine(configuration["SessionKeys:" + requestFor]);
            //Console.WriteLine("........................");  
            return configuration["SessionKeys:" + requestFor];

        }

        public string giveMeBodyForFetchingFlights(FlyDetails fd)
        {
            //fd.date.ToString("yyy-MM-dd")
            string body = "{\"adultPassengersCount\":\"1\",\"adultsCount\":\"1\",\"departureDate\":\"" + fd.date.ToString("yyyy-MM-dd") + "\",\"departureTimeOfDay\":\"ALL_DAY\",\"destinationAirportCode\":\"" + fd.ToCode + "\",\"fareType\":\"USD\",\"from\":\"" + fd.FromCode + "\",\"int\":\"HOMEQBOMAIR\",\"originationAirportCode\":\"" + fd.FromCode + "\",\"passengerType\":\"ADULT\",\"promoCode\":\"\",\"reset\":\"true\",\"returnDate\":\"\",\"returnTimeOfDay\":\"ALL_DAY\",\"to\":\"" + fd.ToCode + "\",\"tripType\":\"oneway\",\"application\":\"air-booking\",\"site\":\"southwest\"}";
            Console.WriteLine(body);
            return body;
        }

        public string getMeReferer(FlyDetails fd)
        {
            string body = "https://www.southwest.com/air/booking/select.html?adultPassengersCount=1&adultsCount=1&departureDate=2023-11-10&departureTimeOfDay=ALL_DAY&destinationAirportCode=&fareType=USD&from=IAD&int=HOMEQBOMAIR&originationAirportCode=IAD&passengerType=ADULT&promoCode=&reset=true&returnDate=&returnTimeOfDay=ALL_DAY&to=MCI&tripType=oneway";
            Console.WriteLine(body);

            return body;
        }
    }
}
