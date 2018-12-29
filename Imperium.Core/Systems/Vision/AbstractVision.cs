using System;
using System.Linq;
using Imperium.CommonData;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Province.Vector;

namespace Imperium.Core.Systems.Vision
{
    public abstract class AbstractVision : Ecs.System
    {
        public event Action OnVisionChanged;
        
        
        
        protected virtual bool[,] GetVisibility(Player player)
        {
            var size = Ecs.SystemManager.GetSystem<Area>().Size;
            var result = new bool[size.X, size.Y];
            
            foreach (var owned in player.OwnedSubjects)
            {
                var placer = owned.Parent.GetComponent<Placer>();
                var observer = owned.Parent.GetComponent<Observer>();

                if (placer == null || observer == null) continue;

                foreach (
                    var position
                    in Vector.Range(
                        Vector.Max(Vector.Zero, placer.Position - observer.VisionRange * Vector.One),
                        Vector.Min(size - Vector.One, placer.Position + observer.VisionRange * Vector.One)))
                {
                    result[position.X, position.Y] = (position - placer.Position).Magnitude <= observer.VisionRange;
                }
            }

            return result;
        }

        public abstract PlaceDto[,] GetCurrentVision(Player player);
    }
}