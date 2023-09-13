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
        public string? place { get; set; }
        public string? addressLine1 { get; set; }
        public string? addressLine2 { get; set; }
        public string? id { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }

    }
}
