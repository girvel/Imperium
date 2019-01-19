using System;

namespace Province.ExtendedFramework
{
    public static class TimeSpanHelper
    {
        public static TimeSpan Multiplied(this TimeSpan span, double k) 
            => TimeSpan.FromMilliseconds(span.TotalMilliseconds * k);

        public static double Divided(this TimeSpan numerator, TimeSpan denominator)
            => numerator.TotalMilliseconds / denominator.TotalMilliseconds;
    }
}