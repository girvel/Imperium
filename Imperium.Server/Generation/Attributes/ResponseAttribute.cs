using System;

namespace Imperium.Server.Generation.Attributes
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