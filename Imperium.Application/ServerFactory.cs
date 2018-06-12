using System;
using System.Collections.Generic;
using System.Net;
using Imperium.Core.Systems.Owning;
using Imperium.Server;

namespace Imperium.Application
{
    public class ServerFactory
    {
        public Server<Player> Generate()
        {
            return new Server<Player>(
                new IPEndPoint(IPAddress.Parse("192.168.0.100"), 7999),
                new RequestManager<Player>(
                    new Dictionary<string, ResponsePair<Player>>(), 
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