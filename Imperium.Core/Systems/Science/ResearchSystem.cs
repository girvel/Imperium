using Imperium.Ecs;

namespace Imperium.Core.Systems.Science
{
    public class ResearchSystem : RegistrationSystem<ResearchHolder>
    {
        public Research RootResearch { get; set; }
        
        
        
        public ResearchSystem(Research rootResearch)
        {
            RootResearch = rootResearch;
        }
    }
}