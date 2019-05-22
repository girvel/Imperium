using System;
using Imperium.Core.Systems.Execution;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs;
using Imperium.Ecs.Attributes;
using Province.Vector;

namespace Imperium.Core.Systems.Movement
{
    [RequiresComponents(typeof(Placer))]
    public class Movable : RegisteredComponent<Movable>
    {
        protected Placer Placer;
        
        public TimeSpan Duration { get; set; }

        public bool Active => CurrentTarget != Placer.Position;

        public Vector CurrentTarget { get; set; }


        public override void Start()
        {
            base.Start();

            Placer = Parent.GetComponent<Placer>();
        }

        public override void Update()
        {
            base.Update();
            
            
        }
    }
}