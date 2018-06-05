using Quartz;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using Quartz.Impl;
using SimpleQuartzApp.Controllers;

namespace SimpleQuartzApp.Models
{
    public class DatabaseScheduler
    {
        public static bool QuartzProcess = false;
        public static bool ScanningProcess = false;

        public void Start()
        {
            MvcApplication.Scheduler.Start();
            DatabaseScanContext obj = new DatabaseScanContext();
            HomeController.i = HomeController.i + 1;
            obj.JobName = "Job" + HomeController.i;
            new DatabaseScheduler().AddTaskJob(obj);
        }

        public void Stop()
        {
            MvcApplication.Scheduler.Shutdown();
        }


        //public BonusJobContext GetBonusJobContext(BonusQuestionModel objQuestion)
        //{
        //    var objJobContext = new BonusJobContext
        //    {
        //        QuestionId = objQuestion.QuestionId,
        //        ClientGameId = objQuestion.ClientGameId,
        //        StartTime = (DateTime)objQuestion.StartTime,
        //        EndTime = (DateTime)objQuestion.EndTime
        //    };
        //    return objJobContext;
        //}
        //public ISimpleTrigger GetDatabaseContextTrigger(string triggerName, DateTime triggerDate, string triggerKeyName)
        //{
        //    ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
        //        .WithIdentity(Regex.Replace(triggerName, @"\s", ""))
        //.StartAt(triggerTime)
        //        .ForJob(Regex.Replace(triggerKeyName, @"\s", ""))
        //        .Build();
        //    return trigger;
        //}

        public ISimpleTrigger GetDatabaseContextTrigger(string triggerName, string triggerKeyName)
        {
            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
                .WithIdentity(Regex.Replace(triggerName, @"\s", ""))
                .WithSimpleSchedule(x=>x.WithIntervalInMinutes(1).RepeatForever())
                .ForJob(Regex.Replace(triggerKeyName, @"\s", ""))
                .Build();
            return trigger;
        }

        //public void CreateJob(JobDataMap jobDataMap, string triggerId, string keyId, DateTime triggerTime)
        public void CreateJob(JobDataMap jobDataMap, string triggerId, string keyId)
        {
            var job = JobBuilder.Create<TaskJob>().WithIdentity(Regex.Replace(keyId, @"\s", "")).UsingJobData(jobDataMap).Build();

            ////Trigger Fired At Specific Time
            //ISimpleTrigger trigger = GetDatabaseContextTrigger(triggerId, triggerTime, keyId);

            //Trigger Fired on App Start
            ISimpleTrigger trigger = GetDatabaseContextTrigger(triggerId, keyId);

            ////Simple Trigger Example
            //ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()
            //    .WithIdentity(Regex.Replace(
            //        triggerId, @"\s", ""))
            //    .StartAt(triggerTime)
            //    .ForJob(Regex.Replace(keyId, @"\s", ""))
            //    .Build();

            MvcApplication.Scheduler.ScheduleJob(job, trigger);
        }

        public void AddTaskJob(DatabaseScanContext objJobContext)
        {
            Random rnd = new Random();
            int val = rnd.Next(1, 10000);
            var jobDataMap = new JobDataMap { { "DemoContext", objJobContext } };
            //For Start Time
            //CreateJob(jobDataMap, "StartTrigger_" + val, "StartKey_" + val, DateTime.Now.AddSeconds(2));
            CreateJob(jobDataMap, "StartTrigger_" + val, "StartKey_" + val);
        }
    }

    public class TaskJob : IJob
    {

        public void Execute(IJobExecutionContext context)
        {
            var objJobContext = (DatabaseScanContext)context.JobDetail.JobDataMap["DemoContext"];
            DatabaseScan(objJobContext);
        }

        #region  Check Database Scan

        private void DatabaseScan(DatabaseScanContext objJobContext)
        {
            if (DatabaseScheduler.QuartzProcess)
            {
                return;
            }
            DatabaseScheduler.QuartzProcess = true;
            #region Check Scanning Process 
            //DatabaseScheduler.ScanningProcess = false; //If process is false than scan and if true than not scan
            if (DatabaseScheduler.ScanningProcess)
            {
                return;
            }
            #endregion
            for (int i = 0; i < 100; i++)
            {
                Debug.WriteLine(objJobContext.JobName + " :: " + i);
                Thread.Sleep(500);
            }
            DatabaseScheduler.QuartzProcess = false;
            DatabaseScheduler.ScanningProcess = false;
        }

        
        #endregion
    }
    public class DatabaseScanContext
    {
        public string JobName { get; set; }
    }


}