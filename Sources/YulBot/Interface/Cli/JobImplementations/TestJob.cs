using System;
using System.Threading.Tasks;
using YulBot.Background.Jobs.JobsInfrastructure.Attributes;
using YulBot.Background.Jobs.JobsInfrastructure.JobsInfrastructure;

namespace YulBot.Interface.Cli.JobImplementations
{
    [CronExpression("0/5 0/1 21 * * ?")]
    [ContextName("Test")]
    public class TestJob : YulJob
    {
        protected override async Task Execute()
        {
            await TelegramClient.SendMessageAsync($"Test - {DateTime.Now:u}");
        }
    }
}