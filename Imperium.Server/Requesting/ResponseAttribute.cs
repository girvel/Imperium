using System;

namespace Imperium.Server.Requesting
{
    public class ResponseAttribute : Attribute
    {
        public string[] Groups { get; }
        
        public ResponseAttribute(params string[] groups)
        {
            Groups = groups;
        }
    }
}