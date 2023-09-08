namespace PlanMyDayApp.Model.Uber
{
    public class LocationFindPost
    {
        public string? type { get; set; }
        // type of journey - pickup or anything else
        public string? location { get; set; }
        //location entered
        public string locale { 
            get { return "en"; }
        }

        public string? lat { get; set; }
        public string? lng { get; set; }

    }
}
