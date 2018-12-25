using System;
using Imperium.Core.Systems.Placing;

namespace Imperium.Game.Generation
{
    public interface IAreaSubgenerator
    {
        void Generate(Area area, Random random);
    }
}