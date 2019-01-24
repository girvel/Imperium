using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Science;
using Imperium.Ecs.Managers;
using Imperium.Game;
using Imperium.Server;
using Imperium.Server.Generation;
using Imperium.Server.Generation.Attributes;

namespace Imperium.Application.Server.Responses
{
    [ResponseContainer]
    public class DebugResponses : IRequestContainer<EcsManager>
    {
        public EcsManager GlobalData { get; set; }
        
        
        
        [Response(Permission.User)]
        public bool AddResources(Connection<Owner> connection)
        {
            connection.Account.ExternalData.Resources += new Resources {Wood = 100};
            return true;
        }
        
        
        
        [Response(Permission.User)]
        public int GetTechnologiesCount(Connection<Owner> connection)
        {
            return connection.Account.ExternalData.Parent.GetComponent<ResearchHolder>().ResearchedTechnologies.Count;
        }
    }
}