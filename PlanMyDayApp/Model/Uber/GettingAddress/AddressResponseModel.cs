namespace PlanMyDayApp.Model.Uber.GettingAddress
{
    // this is only for single address request
    public class AddressResponseModel
    {
        public string? Status { get; set; }
        public Data? data { get; set; }
    }

    public class Data
    {
        public string? type { get; set; }
        public List<AddressObject>? candidates { get; set; }
    }

    public class AddressObject
    {
        public string? id { get; set; }
        public string? addressLine1 { get; set; }
        public string? addressLine2 { get; set; }
        public string? provider { get; set; }
    }

    public class ResponseWithIDandAddress
    {
        public string? id { get; set;}
        public string? address{ get; set; }

        public ResponseWithIDandAddress(string? id, string? address)
        {
            this.id = id;
            this.address = address;
        }
    }


    // for Address co-ordinates response
    public class ResponseForCooridnatesForAddress
    {
        public string? status { get; set; }
        public Location? location { get; set; }
    }

}