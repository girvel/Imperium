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
        public static GameData Game;
        
        public static Server<Player> Server;
        
        
        
        public static void Main(string[] consoleArgs)
        {
            Game = new GameFactory().Generate();
            Server = new ServerFactory().Generate(Game);

            new Thread(Server.Start).Start();
            Game.Ecs.Start();
        }
    }
}