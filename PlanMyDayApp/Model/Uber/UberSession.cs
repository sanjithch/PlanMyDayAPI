using PlanMyDayApp.Model.Uber.GettingAddress;

namespace PlanMyDayApp.Model.Uber
{
    public class UberSession
    {
        // initial request for Address - gives 5 address
        public AddressResponseModel? addressResponseModelforTo { get ; set; }
        public AddressResponseModel? addressResponseModelforFrom { get; set; }

        // after selecting addresses in dropdown
        public JourneyModel? journeyModel { get; set; }
    }
}
