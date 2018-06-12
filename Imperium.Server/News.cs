using System.Collections.Generic;

namespace Imperium.Server
{
    public class News
    {
        public string Type { get; set; }

        public Dictionary<string, dynamic> Info { get; set; }



        internal News() { }
    }
}