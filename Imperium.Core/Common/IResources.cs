using System;

namespace Imperium.Core.Common
{
    public interface IResources : ICloneable
    {
        float[] ResourcesArray { get; }

        float this[int index] { get; set; }

        int ResourceTypesNumber { get; }

        bool Enough(IResources resources);
    }
}