namespace PlanMyDayApp.Model.Uber.GettingAddress
{
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
}