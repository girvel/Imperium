using System;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;

namespace Imperium.Game.Generation.Subgenerators
{
    public class Pregenerator : IAreaGenerator
    {
        public void Generate(Area area, EcsManager ecs, Random random)
        {
            foreach (var position in area.Size.Range())
            {
                area.Move(ecs.EntityManager.Create(Building.Plain).GetComponent<Position>(), position);
            }
        }
    }
}