using System;
using System.Threading.Tasks;
using YulBot.Background.Jobs.Building;
using YulBot.Infrastructure.Common.Configurations;
using YulBot.Infrastructure.Common.Utils;
using YulBot.Interface.Cli.JobImplementations;
using YulBot.Services.Common.Utils;

namespace YulBot.Interface.Cli
{
    class Startup
    {
        static void Main(string[] args)
        {
            Go().Wait();

            var key = Console.ReadLine();
        }

        private static async Task Go()
        {
            YulSettings settings = SettingsBuilder.Instance()
                .AddSettingsFile("yulsettings.json")
                .AddSettingsFile($"yulsettings.{EnvironmentUtils.GetCurrentEnvironment}.json", true)
                .AddSettingsFile("yulsettings.Personal.json", true)
                .Build<YulSettings>();
            
            await YulJobsBuilder.Create(settings.TelegramConfigurations)
                .AddJob<TestJob>()
                .Start();
        }
    }
}