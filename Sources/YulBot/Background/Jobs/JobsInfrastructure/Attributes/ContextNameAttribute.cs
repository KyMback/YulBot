using System;

namespace YulBot.Background.Jobs.JobsInfrastructure.Attributes
{
    public class ContextNameAttribute: Attribute
    {
        public string Name { get; }
        
        public ContextNameAttribute(string name)
        {
            Name = name;
        }
    }
}