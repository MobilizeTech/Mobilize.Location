using System;
using System.Threading.Tasks;
using Mobilize.Common.Http;
using Mobilize.Location.Entities;
using NodaTime;

namespace Mobilize.Location.Helpers
{
    public static class SunriseSunsetApiHelper
    {
        private const string SunriseSunsetApiUrlFormat = "http://api.sunrise-sunset.org/json?lat={0}&lng={1}&date=today&formatted=0";

        public async static Task<Tuple<DateTime, DateTime>> GetSunriseSunsetUtc(double latitude, double longitude)
        {
            try
            {
                string url = string.Format(SunriseSunsetApiUrlFormat, latitude, longitude);
                var response = await RestClient.GetAsync<SunriseSunsetApiResponse>(url, null, UtcDateTimeZoneJsonSerializer.Settings);
                return new Tuple<DateTime, DateTime>(response.Results.Sunrise, response.Results.Sunset);
            }
            catch
            {
                return null;
            }
        }

        public async static Task<Tuple<DateTime, DateTime>> GetSunriseSunsetTimeZone(double latitude, double longitude, DateTimeZone dtz)
        {
            try
            {
                var sunriseSunsetUtc = await GetSunriseSunsetUtc(latitude, longitude);
                var locationSunrise = TimeZoneHelper.ConvertUtcToLocation(dtz, sunriseSunsetUtc.Item1);
                var locationSunset = TimeZoneHelper.ConvertUtcToLocation(dtz, sunriseSunsetUtc.Item2);
                return new Tuple<DateTime, DateTime>(locationSunrise, locationSunset);
            }
            catch
            {
                return null;
            }
        } 
    }
}