﻿using Autofac;
using Surging.Core.Schedule.Quartz;
using Surging.Core.Schedule.Quartz.Runtime;
using Surging.Core.ServiceHosting.Internal;

namespace Surging.Core.Schedule
{
    public static class ServiceHostBuilderExtensions
    {
        public static IServiceHostBuilder UseSchedule(this IServiceHostBuilder hostBuilder)
        {
            return hostBuilder.MapServices(mapper =>
            {
                var jobEntities = mapper.Resolve<IJobEntityProvider>().GetJobEntities();
                var surgingScheduleJobManager = mapper.Resolve<ISurgingScheduleJobManager>();
                foreach (var jobEntity in jobEntities)
                {
                    surgingScheduleJobManager.ScheduleAsync(jobEntity).Wait();
                }
                surgingScheduleJobManager.Start().Wait();
            });
        }
    }
}