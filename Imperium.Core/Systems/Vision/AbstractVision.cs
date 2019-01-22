using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Imperium.CommonData;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;
using Imperium.Ecs.Attributes;
using Province.Vector;
 
namespace Imperium.Core.Systems.Vision
{
    [RequiresSystems(typeof(Area), typeof(Ownership))]
    public abstract class AbstractVision : Ecs.System
    {
        public event Action<Owner, VisionDto> OnVisionChanged;


        
        public override void Start()
        {
            base.Start();

            void UpdatePlayer(Owner player) => OnVisionChanged?.Invoke(player, GetCurrentVision(player));

            void Update(Placer placer)
            {
                foreach (
                    var player 
                    in Ecs.SystemManager
                        .GetSystem<Ownership>().Subjects
                        .Where(p => IsVisible(p, placer.Position)))
                {
                    UpdatePlayer(player);
                }
            }
            
            var area = Ecs.SystemManager.GetSystem<Area>();
            area.OnRemove += Update;
            area.OnPlace += Update;

            Ecs.SystemManager.GetSystem<Ownership>().OnPlayerCreated += p =>
            {
                p.OnOwnedAdded   += o => UpdatePlayer(p);
                p.OnOwnedRemoved += o => UpdatePlayer(p);
            };
        }


        
        protected virtual bool IsVisible(Owner owner, Vector position)
        {
            return owner.OwnedSubjects
                .Select(s => new
                {
                    Observer = s.Parent.GetComponent<Observer>(),
                    Placer = s.Parent.GetComponent<Placer>()
                })
                .Where(p => p.Observer != null && p.Placer != null)
                .Any(p => (position - p.Placer.Position).Magnitude <= p.Observer.VisionRange);
        }

        protected virtual bool[,] GetVisibility(Owner owner)
        {
            var size = Ecs.SystemManager.GetSystem<Area>().Size;
            var result = new bool[size.X, size.Y];
            
            foreach (var owned in owner.OwnedSubjects)
            {
                var placer = owned.Parent.GetComponent<Placer>();
                var observer = owned.Parent.GetComponent<Observer>();

                if (placer == null || observer == null) continue;

                foreach (
                    var position
                    in Vector.Range(
                        Vector.Max(Vector.Zero, placer.Position - observer.VisionRange * Vector.One),
                        Vector.Min(size, placer.Position + observer.VisionRange * Vector.One)))
                {
                    result[position.X, position.Y] |= (position - placer.Position).Magnitude <= observer.VisionRange;
                }
            }

            return result;
        }

        public abstract VisionDto GetCurrentVision(Owner owner);
    }
}