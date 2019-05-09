using System;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;

namespace Imperium.Game.Generation
{
    public interface IAreaSubgenerator
    {
        void Generate(Area area, EcsManager ecs, Random random);
    }
}