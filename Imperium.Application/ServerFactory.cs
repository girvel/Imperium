using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Imperium.Core;
using Imperium.Core.Systems.Owning;
using Imperium.Ecs.Managers;
using Imperium.Game;
using Imperium.Server;

namespace Imperium.Application
{
    public class ServerFactory
    {
        public Server<Player> Generate(GameData game)
        {
            return new Server<Player>(
                new IPEndPoint(IPAddress.Parse("192.168.0.100"), 7999),
                new RequestManager<Player>(
                    new Dictionary<string, ResponsePair<Player>>
                    {
                        ["get area"] = new ResponsePair<Player>(
                            (args, c) =>
                            {
                                var result = new string[game.Area.Size.X, game.Area.Size.Y];
                                foreach (var v in game.Area.Size)
                                {
                                    result[v.X, v.Y] = game.Area[v].First().Parent.Name;
                                }
                                
                                return new Dictionary<string, dynamic>
                                {
                                    ["area"] = result,
                                };
                            }, 
                            "")
                    }, 
                    (args, c) => new Dictionary<string, dynamic> { ["error type"] = "permission" },
                    ex => (args, c) =>
                        new Dictionary<string, dynamic>
                        {
                            ["error type"] = "request",
                            ["exception"] = ex,
                        }), 
                new Log(Console.Out));
        }
    }
}