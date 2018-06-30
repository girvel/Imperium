using System;
using System.Collections.Generic;
using Province.Log;

namespace Imperium.Escape
{
    internal class Program
    {
        public static Log Log;

        static Program()
        {
            Log = new Log(Console.Out);
        }
        
        public static void Main(string[] args)
        {
            Log.Message("Imperium.Escape started");
            
            var assembly = typeof()
        }
    }
}