using System;
using System.Collections.Generic;
using Imperium.Core.Common;
using Imperium.Core.Systems.Placing;
using Imperium.Ecs.Managers;

namespace Imperium.Demo
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var game = EcsManager.CreateNew();
            
            game.SystemManager.Register(new AreaSystem(new Vector(5, 5)));
            game.Start();
        }
    }
}