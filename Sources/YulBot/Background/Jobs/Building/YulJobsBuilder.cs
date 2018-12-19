using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using YulBot.Background.Jobs.JobsInfrastructure.Attributes;
using YulBot.Background.Jobs.JobsInfrastructure.JobsInfrastructure;
using YulBot.Infrastructure.Common;
using YulBot.Infrastructure.Common.Configurations;
using YulBot.Services.Common.Telegram;
using YulBot.Services.Common.Utils;

namespace YulBot.Background.Jobs.Building
{
    public class YulJobsBuilder
    {
        private IList<JobInfo> JobInfos { get; }

        private string DefaultGroup => "group1";

        private ITelegramConfigurations TelegramConfigurations { get; }
        
        private YulJobsBuilder(ITelegramConfigurations telegramConfigurations)
        {
            JobInfos = new List<JobInfo>();
            TelegramConfigurations = telegramConfigurations;
        }

        public static YulJobsBuilder Create(ITelegramConfigurations telegramConfigurations)
        {
            return new YulJobsBuilder(telegramConfigurations);
        }

        public YulJobsBuilder AddJob<TJob>() where TJob: YulJob
        {
            Type type = typeof(TJob);
            
            IJobDetail job = JobBuilder.Create<TJob>()
                .WithIdentity(type.FullName, DefaultGroup)
                .SetJobData(GetJobData(type))
                .Build();

            var cronExpr =
                (CronExpressionAttribute) type.GetCustomAttributes(typeof(CronExpressionAttribute), false).Single();
            
            ITrigger trigger = TriggerBuilder.Create()
                .WithCronSchedule(cronExpr.CronExpression, cron => cron.InTimeZone(TimeZoneInfo.Utc))
                .Build();
             
            JobInfos.Add(new JobInfo(job, trigger));
            
            return this;
        }

        private JobDataMap GetJobData(Type jobType)
        {
            var cronExpr =
                (ContextNameAttribute) jobType.GetCustomAttributes(typeof(ContextNameAttribute), false).Single();
            Context context = TelegramConfigurations.Contexts.Single(c => c.ContextName == cronExpr.Name);
            
            return new JobDataMap
            {
                [SystemConstants.TelegramContextKey] = GetTelegramContext(context)
            };
        }

        private TelegramClient GetTelegramContext(Context context)
        {
            return new TelegramClient(TelegramConfigurations.TelegramApiToken, context.Channel.ChannelId);
        }

        public async Task Start()
        {
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            
            IScheduler scheduler = await new StdSchedulerFactory(props).GetScheduler();
            foreach (var jobInfo in JobInfos)
            {
                await scheduler.ScheduleJob(jobInfo.JobDetail, jobInfo.Trigger);
            }
            
            await scheduler.Start();
        }
    }
}