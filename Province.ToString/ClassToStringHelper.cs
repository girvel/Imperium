using System.Linq;
using System.Net;
using System.Reflection;

namespace Province.ToString
{
    public static class ClassToStringHelper
    {
        public static string ToRepresentativeString(this object o)
        {
            var values
                = o.GetType()
                    .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(f => f.GetCustomAttributes(typeof(RepresentativeAttribute), true).Any())
                    .Select(f => $"{f.Name}: {f.GetValue(o).ToInternalString()}")
                    .Concat(
                        o.GetType()
                            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                            .Where(p => p.GetCustomAttributes(typeof(RepresentativeAttribute), true).Any())
                            .Select(p => $"{p.Name}: {p.GetValue(o).ToInternalString()}"))
                .ToArray();

            return string.Format(
                "[{0}{1}]",
                o.GetType().Name,
                values.Any()
                    ? " | " + values.Aggregate("", (sum, v) => sum + ", " + v.ToString()).Substring(2)
                    : "");
        }

        private static string ToInternalString(this object o)
            => o is string ? "\"" + o + "\"" : o.ToString();
    }
}