using System;
using System.Linq;
using System.Reflection;
using Imperium.Ecs.Attributes;

namespace Imperium.Ecs
{
    public abstract class RegisteredComponent<TThis> : Component
        where TThis : RegisteredComponent<TThis>
    {
        public RegistrationSystem<TThis> System { get; private set; }
        
        internal ReflectedTimer[] ReflectedTimers;

        public new TThis Prototype
        {
            get => base.Prototype as TThis;
            set => base.Prototype = value;
        }
        
        public override void Start()
        {
            base.Start();

            System = Ecs.SystemManager.GetSystem<RegistrationSystem<TThis>>();
            System.Register((TThis) this);
            
            ReflectedTimers
                = GetType()
                    .GetMethods()
                    .Select(m => (
                        method: m, 
                        attribute: m.GetCustomAttribute<PeriodicalAttribute>()))
                    .Where(t => t.attribute != null)
                    .Select(t => new ReflectedTimer(
                        o => t.method.Invoke(o, new object[0]), 
                        t.method.DeclaringType.GetProperty(t.attribute.CounterFieldName)))
                    .ToArray();
        }

        public virtual void Update()
        {
            foreach (var timer in ReflectedTimers)
            {
                var timerValue = (TimeSpan) timer.TimeProperty.GetValue(this) - Ecs.UpdateDelay;
                
                var step 
                    = (TimeSpan) timer.TimeProperty.GetValue(
                        timer.TimeProperty.DeclaringType
                            .GetProperty("Prototype", typeof(Component))
                            .GetValue(this));

                while (timerValue < TimeSpan.Zero)
                {
                    timer.Action(this);
                    timerValue += step;
                }
                
                timer.TimeProperty.SetValue(this, timerValue);
            }
        }

        internal class ReflectedTimer
        {
            public Action<object> Action;

            public PropertyInfo TimeProperty;

            public ReflectedTimer(Action<object> action, PropertyInfo timeProperty)
            {
                Action = action;
                TimeProperty = timeProperty;
            }
        }
    }
}