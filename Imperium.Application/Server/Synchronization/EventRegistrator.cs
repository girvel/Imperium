using System.Linq;
using Imperium.Application.Common;
using Imperium.CommonData;
using Imperium.Core.Systems.Landscape;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;
using Imperium.Game.Prototypes;
using Imperium.Game.Systems.Vision;
using Imperium.Server;
using NetData = System.Collections.Generic.Dictionary<string, object>;

namespace Imperium.Application.Server.Synchronization
{
    public static class EventRegistrator
    {
        public static void Register(Server<Player> server, EcsManager gameData)
        {
            gameData.SystemManager.GetSystem<ClientVision>().OnVisionChanged += (player, vision) =>
            {
                server.NewsManager.AddNews(
                    player,
                    "OnVisionChanged",
                    new NetData
                    {
                        ["vision"] = vision,
                    },
                    unical: true);
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