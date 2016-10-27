using System;
using Mobilize.Common.Http;
using Mobilize.Location.Entities;
using NodaTime;

namespace Mobilize.Location.Helpers
{
    public class GoogleTimeZoneApiHelper
    {
        private const string GoogleTimeZoneApiUrlFormat = "https://maps.googleapis.com/maps/api/timezone/json?location={0},{1}&timestamp={2}&key={3}";

        public static DateTimeZone GetDateTimeZone(string apiKey, double latitude, double longitude)
        {
            try
            {
                long timestamp = GetTimestamp();
                string url = string.Format(GoogleTimeZoneApiUrlFormat, latitude, longitude, timestamp, apiKey);
                var response = RestClient.Get<GoogleTimeZoneApiResponse>(url);
                return DateTimeZoneProviders.Tzdb.GetZoneOrNull(response.TimeZoneId);
            }
            catch
            {
                return null;
            }
        }

        private static long GetTimestamp()
        {
            return (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}