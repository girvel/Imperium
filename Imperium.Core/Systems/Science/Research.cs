namespace Imperium.Core.Systems.Science
{
    public class Research
    {
        public string Name { get; set; }
        
        public double RequiredSciencePoints { get; set; }
        
        public Research[] Children { get; set; }
        
        
        
        public Research(string name, float requiredSciencePoints, Research[] children)
        {
            Name = name;
            RequiredSciencePoints = requiredSciencePoints;
            Children = children;
        }
    }
}