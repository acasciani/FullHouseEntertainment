﻿using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Website.Controllers.Source.Scrapers;

namespace Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = true;
            ScheduleScraping();
        }

        private void ScheduleScraping()
        {
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            InstagramJob(sched);
            FacebookJob(sched);
        }

        private void InstagramJob(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<InstagramScraperJob>()
                .WithIdentity("GetMedia", "Instagram")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("dailyRun", "Instagram")
                .WithDailyTimeIntervalSchedule(i => i
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                    .EndingDailyAfterCount(1)
                    )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }

        private void FacebookJob(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<FacebookScraperJob>()
                .WithIdentity("GetRatings", "Facebook")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("dailyRun", "Facebook")
                .WithDailyTimeIntervalSchedule(i => i
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 5))
                    .EndingDailyAfterCount(1)
                    )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
