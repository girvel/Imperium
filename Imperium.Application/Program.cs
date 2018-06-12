using System;
using System.Collections.Generic;
using Imperium.Core.Common;
using Imperium.Core.Systems.Owning;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;
using Imperium.Game;

namespace Imperium.Application
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var game = EcsManager.CreateNew();
            
            game.SystemManager.Register(new AreaSystem(new Vector(5, 5)));
            game.SystemManager.Register(new PlayerSystem());
            
            new TestWorldFactory().Generate(game.SystemManager.GetSystem<AreaSystem>());
            
            game.Start();
        }
    }
}