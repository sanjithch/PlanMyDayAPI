namespace PlanMyDayApp.Model.Uber.GettingAddress
{
    public class JourneyModel
    {
        public Location? From { get; set; }
        public Location? To { get; set; }
    }

    // this is for each address details
    public class Location
    {
        public string? id { get; set; }
        public string? addressLine1 { get; set; }   
        public string? addressLine2 { get; set; }
        public string? fullAddress { get; set; }
        public string? title { get; set; }
        public string? provider { get; set; }
        public float lat { get; set; }
        public float Long { get; set; }
        public string? type { get; set; }

    }

    // for receieving 
    public class JourneyCoOrdinates
    {
        public List<CoOrdinates> destinations { get; set; }
        public CoOrdinates pickup { get; set; }
    }

    public class CoOrdinates
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
