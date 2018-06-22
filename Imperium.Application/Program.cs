using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Imperium.Application.Server;
using Imperium.Core.Systems.Owning;
using Imperium.Ecs.Managers;
using Imperium.Game;
using Imperium.Server;
using Imperium.Server.Generation;

namespace Imperium.Application
{
    internal class Program
    {
        public static GameData Game;
        
        public static Server<Player> Server;
        
        
        
        public static void Main(string[] consoleArgs)
        {
            Game = new GameFactory().Generate();
            Server = new ServerFactory<Player, GameData>().Generate(Game, typeof(Program).Assembly);
            Server.Accounts.Add(new Account<Player>("", "", new[] {Permission.User, Permission.Admin}, new Player()));

            new Thread(Server.Start).Start();
            Game.Ecs.Start();
        }
    }
}