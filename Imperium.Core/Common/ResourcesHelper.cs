using System;
using System.Linq;

namespace Imperium.Core.Common
{
    public static class ResourcesHelper
    {
        public static T Added<T>(this T r1, T r2) where T : IResources
        {
            var chooseR = r1.ResourceTypesNumber > r2.ResourceTypesNumber;
            var result = chooseR ? (T) r1.Clone() : (T) r2.Clone();

            for (var i = 0; i < result.ResourceTypesNumber; i++)
            {
                result[i] += (chooseR ? r2 : r1)[i];
            }

            return result;
        }

        public static T Inverted<T>(this T r) where T : IResources
        {
            var result = (T) r.Clone();

            for (var i = 0; i < result.ResourceTypesNumber; i++)
            {
                result[i] = -r[i];
            }

            return result;
        }

        public static T Substracted<T>(this T r1, T r2) where T : IResources
        {
            return r1.Added(r2.Inverted());
        }

        public static T Multiplied<T>(this T r, float n) where T : IResources
        {
            var result = (T) r.Clone();

            for (var i = 0; i < r.ResourceTypesNumber; i++)
            {
                result[i] *= n;
            }

            return result;
        }

        public static bool Equals<T>(this T r1, T r2) where T : IResources
        {
            int max = Math.Max(r1.ResourceTypesNumber, r2.ResourceTypesNumber),
                min = Math.Min(r1.ResourceTypesNumber, r2.ResourceTypesNumber);
            
            for (var i = 0; i < min; i++)
            {
                if (r1[i] != r2[i])
                {
                    return false;
                }
            }

            for (var i = min; i < max; i++)
            {
                if (!(r1.ResourceTypesNumber > r2.ResourceTypesNumber && r1[i] == 0 
                      || r1.ResourceTypesNumber < r2.ResourceTypesNumber && r2[i] == 0))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Enough<T>(this T resources, T price) where T : IResources
        {
            return resources.Substracted(price).ResourcesArray.All(r => r >= 0);
        }

        public static float GetEnoughCoefficient<T>(this T resources, T price) where T : IResources
        {
            var result = 1F;

            for (var i = 0; i < resources.ResourceTypesNumber; i++)
            {
                if (price[i] <= 0) continue;

                var current = resources[i] / price[i];

                if (current < result)
                {
                    result = current;
                }
            }

            return result;
        }

        public static bool IsZero<T>(this T resources) where T : IResources 
            => resources?.ResourcesArray.All(r => r == 0) ?? true;
    }
}