using System;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;

namespace Imperium.Game.Generation
{
    public interface IAreaGenerator
    {
        void Generate(Area area, EcsManager ecs, Random random);
    }
}