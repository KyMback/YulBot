using System;

namespace YulBot.Background.Jobs.JobsInfrastructure.Attributes
{
    public class CronExpressionAttribute: Attribute
    {
        public string CronExpression { get; }
        
        public CronExpressionAttribute(string cronExpression)
        {
            CronExpression = cronExpression;
        }
    }
}