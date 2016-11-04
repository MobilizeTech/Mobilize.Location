using System;
using System.Threading.Tasks;
using Mobilize.Common.Http;
using Mobilize.Location.Entities;
using NodaTime;

namespace Mobilize.Location.Helpers
{
    public class GoogleTimeZoneApiHelper
    {
        private const string GoogleTimeZoneApiUrlFormat = "https://maps.googleapis.com/maps/api/timezone/json?location={0},{1}&timestamp={2}&key={3}";

        public async static Task<DateTimeZone> GetDateTimeZone(string apiKey, double latitude, double longitude)
        {
            var timeZoneId = await GetDateTimeZoneId(apiKey, latitude, longitude);
            return DateTimeZoneProviders.Tzdb.GetZoneOrNull(timeZoneId);
        }

        public async static Task<string> GetDateTimeZoneId(string apiKey, double latitude, double longitude)
        {
            try
            {
                long timestamp = GetTimestamp();
                string url = string.Format(GoogleTimeZoneApiUrlFormat, latitude, longitude, timestamp, apiKey);
                var response = await RestClient.GetAsync<GoogleTimeZoneApiResponse>(url);
                return response.TimeZoneId;
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