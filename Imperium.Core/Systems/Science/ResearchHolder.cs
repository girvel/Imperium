using System.Collections.Generic;
using System.Linq;
using Imperium.Core.Systems.Owning;
using Imperium.Ecs;
using Imperium.Ecs.Attributes;

namespace Imperium.Core.Systems.Science
{
    [RequiresComponents(typeof(Owner))]
    public class ResearchHolder : RegisteredComponent<ResearchSystem, ResearchHolder>
    {
        public List<Research> ResearchedTechnologies { get; } = new List<Research>();
        
        public Research CurrentResearch { get; internal set; }
        
        public double CurrentSciencePoints { get; set; }

        public IEnumerable<Researcher> Researchers
            => Parent
                .GetComponent<Owner>()
                .OwnedSubjects
                .Select(s => s.Parent.GetComponent<Researcher>())
                .Where(r => r != null);



        public bool BeginResearch(Research research)
        {
            var success = !ResearchedTechnologies.Contains(research) 
                          && (research == System.RootResearch 
                              || ResearchedTechnologies.Any(r => r.Children.Contains(research)));

            if (success) CurrentResearch = research;

            return success;
        }
    }
}