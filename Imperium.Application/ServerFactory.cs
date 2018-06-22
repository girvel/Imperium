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
using Response = Imperium.Server.ResponsePair<Imperium.Core.Systems.Owning.Player>;
using NetData = System.Collections.Generic.Dictionary<string, dynamic>;

namespace Imperium.Application
{
    public class ServerFactory
    {
        public Server<Player> Generate(GameData game)
        {
            return new Server<Player>(
                new IPEndPoint(IPAddress.Parse("192.168.0.100"), 7999),
                new RequestManager<Player>(
                    new Dictionary<string, Response>
                    {
                        ["get area"] = new Response(
                            (args, c) =>
                            {
                                var result = new string[game.Area.Size.X, game.Area.Size.Y];
                                foreach (var v in game.Area.Size)
                                {
                                    result[v.X, v.Y] = game.Area[v].First().Parent.Name;
                                }
                                
                                return new NetData
                                {
                                    ["area"] = result,
                                };
                            }, 
                            "user"),
                        
                        ["login"] = new ResponsePair<Player>(
                            (args, c) =>
                            {
                                var account = c.Server.Accounts.FirstOrDefault(a =>
                                    a.Login == args["login"] && a.Password == args["password"]);

                                if (c.Server.Connections.All(connection => connection.Account != account))
                                {
                                    c.Account = account;
                                }

                                return new NetData
                                {
                                    ["success"] = account != null,
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
                new Log(Console.Out))
            {
                Accounts = {new Account<Player>("", "", new []{"user"}, new Player())}
            };
        }
    }
}