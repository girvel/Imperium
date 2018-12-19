using System;
using Imperium.Core.Systems.Placing;

namespace Imperium.Game.Generation
{
    public interface IAreaSubgenerator
    {
        void Generate(AreaSlice buildingSlice, AreaSlice landscapeSlice, Random random);
    }
}