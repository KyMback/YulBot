using Quartz;

namespace YulBot.Background.Jobs.Building
{
    public class JobInfo
    {
        public JobInfo()
        {
        }

        public JobInfo(IJobDetail jobDetail, ITrigger trigger)
        {
            Trigger = trigger;
            JobDetail = jobDetail;
        }
        
        public IJobDetail JobDetail { get; set; }

        public ITrigger Trigger { get; set; }
    }
}