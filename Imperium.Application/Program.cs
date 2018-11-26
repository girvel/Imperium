using System.Threading;
using Imperium.Application.Server.Synchronization;
using Imperium.Core.Systems.Owning;
using Imperium.Ecs.Managers;
using Imperium.Game;
using Imperium.Server;
using Imperium.Server.Generation;

namespace Imperium.Application
{
    internal class Program
    {
        public static EcsManager Ecs;
        
        public static Server<Player> Server;
        
        
        
        public static void Main(string[] consoleArgs)
        {
            Ecs = new EcsFactory().Generate();
            
            Server = new ResponseServerFactory<Player, EcsManager>().Generate(Ecs, typeof(Program).Assembly);

            EventRegistrator.Register(Server, Ecs);

            var player = new Player();
            Server.Accounts.Add(new Account<Player>("", "", new[] {Permission.User, Permission.Admin}, player));
            Ecs.SystemManager.GetSystem<PlayerSystem>().Register(player);

            new Thread(Server.Start).Start();
            Ecs.Start();
        }
    }
}