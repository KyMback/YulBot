using System.Threading.Tasks;
using Quartz;
using YulBot.Infrastructure.Common;
using YulBot.Services.Common.Telegram;

namespace YulBot.Background.Jobs.JobsInfrastructure.JobsInfrastructure
{
    public abstract class YulJob: IJob
    {
        protected TelegramClient TelegramClient { get; private set; }
        
        protected IJobExecutionContext ExecutionContext { get; private set; }
        
        public async Task Execute(IJobExecutionContext context)
        {
            TelegramClient = context.JobDetail.JobDataMap[SystemConstants.TelegramContextKey] as TelegramClient;
            ExecutionContext = context;
            
            await Execute();
        }

        protected abstract Task Execute();
    }
}