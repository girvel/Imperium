using System.Collections.Generic;

namespace Imperium.Core.Systems.Owning
{
    public class Player
    {
        public string Name { get; set; }
        
        public List<OwnedComponent> OwnedSubjects { get; set; } = new List<OwnedComponent>();
    }
}