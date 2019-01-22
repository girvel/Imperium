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

        public override void Update()
        {
            base.Update();

            foreach (var researchHolder in Subjects)
            {
                if (researchHolder.CurrentResearch == null)
                {
                    continue;
                }
                
                foreach (var researcher in researchHolder.Researchers)
                {
                    researchHolder.CurrentSciencePoints 
                        += researcher.SciencePointsPerSecond * Ecs.UpdateDelay.TotalSeconds;
                }

                if (researchHolder.CurrentSciencePoints >= researchHolder.CurrentResearch.RequiredSciencePoints)
                {
                    researchHolder.ResearchedTechnologies.Add(researchHolder.CurrentResearch);
                    researchHolder.CurrentResearch = null;
                    researchHolder.CurrentSciencePoints = 0;
                }
            }
        }
    }
}