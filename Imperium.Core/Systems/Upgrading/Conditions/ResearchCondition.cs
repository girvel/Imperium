using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Science;
using Imperium.Ecs;

namespace Imperium.Core.Systems.Upgrading.Conditions
{
    public class ResearchCondition : Condition
    {
        public Research RequiredResearch { get; }
        
        public ResearchCondition(Research requiredResearch)
        {
            RequiredResearch = requiredResearch;
        }

        public override bool IsPossible(Entity @from, Owner owner)
        {
            return owner.Parent.GetComponent<ResearchHolder>().ResearchedTechnologies.Contains(RequiredResearch);
        }

        public override void Apply(Entity @from, Owner owner)
        {
            
        }
    }
}