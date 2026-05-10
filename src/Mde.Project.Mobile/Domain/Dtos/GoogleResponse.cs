namespace Mde.Project.Mobile.Domain.Dtos
{
    public class GoogleResponse
    {
        // geneste klassen
        public string status { get; set; }
        public List<Result> results { get; set; }

        public class Result
        {
            public Geometry geometry { get; set; }
            public string formatted_address { get; set; }
            public List<AddressComponent> address_components { get; set; }
        }

        public class Geometry
        {
            public Location location { get; set; }
        }

        public class Location
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class AddressComponent
        {
            public string long_name { get; set; }
            public List<string> types { get; set; }
        }
    }
}
