using System;
using System.Collections.Generic;
using Imperium.Ecs.Managers;

namespace Imperium.Demo
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var game = EcsManager.CreateNew();
            
            game.Start();
        }
    }
}