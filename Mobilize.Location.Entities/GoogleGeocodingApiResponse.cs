namespace Mobilize.Location.Entities
{
    public class GoogleGeocodingApiResponse
    {
        public string Status { get; set; }

        public GoogleGeocodingApiResults[] Results { get; set; }
    }

    public class GoogleGeocodingApiResults
    {
        public string Formatted_Address { get; set; }

        public GoogleGeocodingApiGeometry Geometry { get; set; }
    }

    public class GoogleGeocodingApiGeometry
    {
        public GoogleGeocodingApiLocation Location { get; set; }
    }

    public class GoogleGeocodingApiLocation
    {
        public double Lat { get; set; }

        public double Lng { get; set; }
    }
}