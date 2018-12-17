using System;

namespace DemoCron.Helpers
{
    internal static class ConfigExtensions
    {
        public static TimeSpan ToTimeSpan(this Settings.TimeSpanSettings settings)
        {
            var days = settings.Days ?? 0;
            var hours = settings.Hours ?? 0;
            var minutes = settings.Minutes ?? 0;
            var seconds = settings.Seconds ?? 0;

            return new TimeSpan(days, hours, minutes, seconds);
        }
    }
}
