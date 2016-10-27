using System;

namespace Mobilize.Location.Entities
{
    public class SunriseSunsetApiResponse
    {
        public string Status { get; set; }

        public SunriseSunsetApiResult Results { get; set; }
    }

    public class SunriseSunsetApiResult
    {
        public DateTime Sunrise { get; set; }

        public DateTime Sunset { get; set; }

        public DateTime Solar_Noon { get; set; }

        public int Day_Length { get; set; }

        public DateTime Civil_Twilight_Begin { get; set; }

        public DateTime Civil_Twilight_End { get; set; }

        public DateTime Nautical_Twilight_Begin { get; set; }

        public DateTime Nautical_Twilight_End { get; set; }

        public DateTime Astronomical_Twilight_Begin { get; set; }

        public DateTime Astronomical_Twilight_End { get; set; }
    }
}