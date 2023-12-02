using PlanMyDayApp.Model.Uber.GettingAddress;
using static System.Net.WebRequestMethods;

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

        public string giveMeBodyForFetchingFlights(RequestForFlightDetails fd)
        {
            string body = "{\"originationAirportCode\":\"" + fd.From + "\",\"destinationAirportCode\":\"" + fd.To + "\",\"departureDate\":\"" + fd.date + "\",\"departureTimeOfDay\":\"ALL_DAY\",\"returnTimeOfDay\":\"ALL_DAY\",\"adultPassengersCount\":\"1\",\"tripType\":\"oneway\",\"fareType\":\"USD\",\"passengerType\":\"ADULT\",\"clk\":\"GSUBNAV-AIR-BOOK\",\"adultsCount\":\"1\",\"returnDate\":\"\",\"application\":\"air-booking\",\"site\":\"southwest\"}";
            Console.WriteLine(body);
            return body;
        }

        //public string getMeReferer(RequestForFlightDetails fd)
        //{
        //    //string body = "https://www.southwest.com/air/booking/select.html?adultPassengersCount=1&adultsCount=1&departureDate=2023-11-10&departureTimeOfDay=ALL_DAY&destinationAirportCode=" + fd.ToCode + "&fareType=USD&from=" + fd.FromCode + "&int=HOMEQBOMAIR&originationAirportCode=" + fd.FromCode + "&passengerType=ADULT&promoCode=&reset=true&returnDate=&returnTimeOfDay=ALL_DAY&to=" + fd.ToCode + "&tripType=oneway";
        //    //Console.WriteLine(body);

        //    string body = $" https://www.southwest.com/air/booking/select.html?adultPassengersCount=1&adultsCount=1&departureDate=2023-11-10&departureTimeOfDay=ALL_DAY&destinationAirportCode={fd.To}&fareType=USD&from={fd.From}&int=HOMEQBOMAIR&originationAirportCode={fd.From}&passengerType=ADULT&promoCode=&reset=true&returnDate=&returnTimeOfDay=ALL_DAY&to={fd.To}&tripType=oneway";
        //    Console.WriteLine(body);
        //    //return " https://www.southwest.com/air/booking/select.html?adultPassengersCount=1&adultsCount=1&departureDate=2023-11-10&departureTimeOfDay=ALL_DAY&destinationAirportCode=DCA&fareType=USD&from=MCI&int=HOMEQBOMAIR&originationAirportCode=MCI&passengerType=ADULT&promoCode=&reset=true&returnDate=&returnTimeOfDay=ALL_DAY&to=DCA&tripType=oneway";
        //    return body;
        //}
    }
}
