using System;
using NodaTime;
using NodaTime.TimeZones;

namespace Mobilize.Location.Helpers
{
    public static class TimeZoneHelper
    {
        public static DateTime ConvertUtcToLocation(DateTimeZone dtz, DateTime utc)
        {
            DateTimeZone utcTimeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull("UTC");
            ZoneLocalMappingResolver customResolver = Resolvers.CreateMappingResolver(Resolvers.ReturnLater, Resolvers.ReturnStartOfIntervalAfter);
            var local = LocalDateTime.FromDateTime(utc);
            var utcZoned = utcTimeZone.ResolveLocal(local, customResolver);
            var locationZoned = utcZoned.ToInstant().InZone(dtz).ToDateTimeUnspecified();
            return locationZoned;
        }
    }
}