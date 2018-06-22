using System;

namespace Imperium.Application.Server
{
    public class RequestAttribute : Attribute
    {
        public string[] Groups { get; }
        
        public RequestAttribute(params string[] groups)
        {
            Groups = groups;
        }
    }
}