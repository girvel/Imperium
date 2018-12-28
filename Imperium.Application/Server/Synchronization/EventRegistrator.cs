using System.Linq;
using Imperium.Application.Common;
using Imperium.CommonData;
using Imperium.Core.Systems.Landscape;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;
using Imperium.Game.Prototypes;
using Imperium.Server;
using NetData = System.Collections.Generic.Dictionary<string, object>;

namespace Imperium.Application.Server.Synchronization
{
    public static class EventRegistrator
    {
        public static void Register(Server<Player> server, EcsManager gameData)
        {
            gameData.EntityManager.OnEntityCreate += entity =>
            {
                var placer = entity.GetComponent<Placer>();

                if (!(placer != null && entity ^ typeof(Building))) return;

                var area = gameData.SystemManager.GetSystem<Area>();
                
                foreach (var player in server.Connections.Select(c => c.Account.ExternalData))
                {
                    server.NewsManager.AddNews(
                        player, 
                        "OnEntityCreate", 
                        new NetData
                        {
                            ["dto"] = area.GetPlaceDto(placer.Position),
                            ["position"] = placer.Position,
                        });
                }
            };
            
            gameData.EntityManager.OnEntityDestroy += entity =>
            {
                var positionComponent = entity.GetComponent<Placer>();

                if (positionComponent == null) return;
                
                foreach (var player in server.Connections.Select(c => c.Account.ExternalData))
                {
                    server.NewsManager.AddNews(
                        player, 
                        "OnEntityDestroy", 
                        new NetData
                        {
                            ["name"] = entity.Name,
                            ["position"] = positionComponent.Position,
                        });
                }
            };

            gameData.SystemManager.GetSystem<PlayerSystem>().OnPlayerCreated += player =>
            {
                player.OnResourcesChanged += resources =>
                {
                    server.NewsManager.AddNews(
                        player,
                        "OnResourcesChanged",
                        new NetData
                        {
                            ["value"] = resources.ResourcesArray,
                        },
                        unical: true);
                };
            };
        }
    }
}