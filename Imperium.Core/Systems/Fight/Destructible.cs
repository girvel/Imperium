using Imperium.Ecs;

namespace Imperium.Core.Systems.Fight
{
    public class Destructible : Component
    {
        private float _healthPoints;

        public Destructible(float healthPoints)
        {
            HealthPoints = healthPoints;
        }

        public virtual void Damage(float delta)
        {
            HealthPoints -= delta;
        }

        public float HealthPoints
        {
            get => _healthPoints;
            protected set
            {
                _healthPoints = value;

                if (_healthPoints <= 0)
                {
                    Ecs.EntityManager.Destroy(Parent);
                }
            }
        }
    }
}