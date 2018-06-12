using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Imperium.Core.Systems.Owning;
using Imperium.Ecs.Managers;
using Imperium.Game;
using Imperium.Server;

namespace Imperium.Application
{
    internal class Program
    {
        public static EcsManager Ecs;
        
        public static Server<Player> Server;
        
        
        
        public static void Main(string[] consoleArgs)
        {
            Ecs = new EcsFactory().Generate();
            Server = new ServerFactory().Generate();

            new Thread(Server.Start).Start();
            Ecs.Start();
        }
    }
}