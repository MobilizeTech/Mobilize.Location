using System;
using System.Net.Http;
using Newtonsoft.Json;
using Mobilize.Location.Entities;
using NodaTime;

namespace Mobilize.Location.Helpers
{
    public static class SunriseSunsetApiHelper
    {
        private const string SunriseSunsetApiUrlFormat = "http://api.sunrise-sunset.org/json?lat={0}&lng={1}&date=today&formatted=0";

        private static JsonSerializerSettings UtcDateTimeZoneHandlingSettings
        {
            get
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                return settings;
            }
        }

        public static Tuple<DateTime, DateTime> GetSunriseSunset(double latitude, double longitude, DateTimeZone dtz)
        {
            try
            {
                string url = string.Format(SunriseSunsetApiUrlFormat, latitude, longitude);
                using (var client = new HttpClient())
                {
                    var result = client.GetAsync(url).Result;
                    string json = result.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<SunriseSunsetApiResponse>(json, UtcDateTimeZoneHandlingSettings);

                    // Extract UTC sunrise/sunset
                    var sunriseUtc = response.Results.Sunrise;
                    var sunsetUtc = response.Results.Sunset;

                    // Convert to location sunrise/sunset
                    var sunriseLocation = TimeZoneHelper.ConvertUtcToLocation(dtz, sunriseUtc);
                    var sunsetLocation = TimeZoneHelper.ConvertUtcToLocation(dtz, sunsetUtc);

                    return new Tuple<DateTime, DateTime>(sunriseLocation, sunsetLocation);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}