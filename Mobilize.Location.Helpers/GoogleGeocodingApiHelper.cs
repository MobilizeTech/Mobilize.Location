using System;
using System.Linq;
using Mobilize.Common.Http;
using Mobilize.Location.Entities;

namespace Mobilize.Location.Helpers
{
    public class GoogleGeocodingApiHelper
    {
        private const string GoogleGeocodingApiUrlFormat = "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}";

        public static Tuple<double, double> GetCoordinates(string apiKey, string address)
        {
            try
            {
                string escapedAddress = Uri.EscapeDataString(address);
                string url = string.Format(GoogleGeocodingApiUrlFormat, escapedAddress, apiKey);
                var response = RestClient.Get<GoogleGeocodingApiResponse>(url);
                var location = response.Results.First().Geometry.Location;
                return new Tuple<double, double>(location.Lat, location.Lng);
            }
            catch
            {
                return null;
            }
        }
    }
}