using Microsoft.Phone.Scheduler;
using MonthlyCycleApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPControls.Models;

namespace MonthlyCycleApp.NotificationsAndTiles
{
    public class RemindersManager
    {

        public static Uri appUri = new Uri(AppResources.ApplicationInitialUri, UriKind.Relative);

        public static void AddMenstruationReminder(DateTime beginTime, DateTime endTime)
        {
            AddOrReplaceReminder(
                AppResources.MenstruationReminderName,
                AppResources.ApplicationTitle,
                AppResources.MenstruationReminderContent,
                beginTime,
                endTime,
                RecurrenceInterval.None,
                appUri);
        }

        public static void AddOvulationReminder(DateTime beginTime, DateTime endTime)
        {
            AddOrReplaceReminder(
                 AppResources.OvulationReminderName,
                 AppResources.ApplicationTitle,
                 AppResources.OvulationReminderContent,
                 beginTime,
                 endTime,
                 RecurrenceInterval.None,
                 appUri);
        }

        public static void AddGyneReminder(DateTime beginTime, RecurencePeriod recPeriod)
        {
            RecurrenceInterval interval = RecurrenceInterval.None;
            DateTime expirationTime = DateTime.MaxValue;

            switch (recPeriod.ReccurenceType)
            {
                case RecurencePeriodType.Yearly:
                    {
                        interval = RecurrenceInterval.Yearly;
                        break;
                    }
                case RecurencePeriodType.SixMonths:
                    {
                        expirationTime = beginTime.AddMonths(6);
                        interval = RecurrenceInterval.None;
                        break;
                    }

                case RecurencePeriodType.ThreeMonths:
                    {
                        interval = RecurrenceInterval.None;
                        expirationTime = beginTime.AddMonths(3);
                        break;
                    }
                case RecurencePeriodType.Monthly:
                    {
                        interval = RecurrenceInterval.Monthly;
                        break;
                    }
                case RecurencePeriodType.OneTime:
                    {
                        interval = RecurrenceInterval.None;
                        expirationTime = beginTime.AddDays(1);
                        break;
                    }
            }

            AddOrReplaceReminder(
                                AppResources.GyneControlReminderName,
                                AppResources.ApplicationTitle,
                                string.Format(AppResources.GyneControlReminderContent, interval),
                                beginTime,
                                expirationTime,
                                interval,
                                appUri);
        }

        public static void AddBreastReminder(DateTime beginTime, DateTime endTime, RecurrenceInterval interval)
        {
            AddOrReplaceReminder(
                 AppResources.OvulationReminderName,
                 AppResources.ApplicationTitle,
                 string.Format(AppResources.BreastControlReminderContent, interval),
                 beginTime,
                 endTime,
                 RecurrenceInterval.Yearly,
                 appUri);
        }

        public static void AddPillAlarm(PeriodMonth period)
        {
            AddOrReplaceAlarm(
                AppResources.PillAlarmName,
                AppResources.PillAlarmContent,
                period.PeriodEndDay.AddDays(1),
                period.CycleEndDay,
                RecurrenceInterval.Daily,
                new Uri("/Reminder/Reminder01.wav", UriKind.Relative));

        }
        public static void AddPillAlarm(DateTime beginTime, DateTime endTime)
        {
            AddOrReplaceAlarm(
                AppResources.PillAlarmName,
                AppResources.PillAlarmContent,
                beginTime,
                endTime,
                RecurrenceInterval.Daily,
                new Uri("/Reminder/Reminder01.wav", UriKind.Relative));
        }

        public static void AddReminder(
                                        string name,
                                        string title,
                                        string content,
                                        DateTime beginTime,
                                        DateTime expirationTime,
                                        RecurrenceInterval recurrenceType,
                                        Uri navigationUri)
        {
            ScheduledAction action = ScheduledActionService.Find(name);
            if (action == null)
            {
                Reminder reminder = new Reminder(name);
                reminder.Title = title;
                reminder.Content = content;
                reminder.BeginTime = beginTime;
                reminder.ExpirationTime = expirationTime;
                reminder.RecurrenceType = recurrenceType;
                reminder.NavigationUri = navigationUri;
                // Register the reminder with the system.
                ScheduledActionService.Add(reminder);
            }
        }

        public static void AddOrReplaceReminder(
            string name,
            string title,
            string content,
            DateTime beginTime,
            DateTime expirationTime,
            RecurrenceInterval recurrenceType,
            Uri navigationUri)
        {
           
             Reminder reminder = new Reminder(name);
             reminder.Title = title;
             reminder.Content = content;
             reminder.BeginTime = beginTime;
             reminder.ExpirationTime = expirationTime;
             reminder.RecurrenceType = recurrenceType;
             reminder.NavigationUri = navigationUri;

             ScheduledAction action = ScheduledActionService.Find(name);
             if (action == null) 
                 // Register the reminder with the system.
                 ScheduledActionService.Add(reminder);
             else
                 ScheduledActionService.Replace(reminder);
        }

        public static void AddOrReplaceAlarm(
            string name,
            string content,
            DateTime beginTime,
            DateTime expirationTime,
            RecurrenceInterval recurrenceType,
            Uri alarmSound ) 
        {
             ScheduledAction action = ScheduledActionService.Find(name);

             Alarm alarm = new Alarm(name);
             alarm.Content = content;
             alarm.Sound = alarmSound;
             alarm.BeginTime = beginTime;
             alarm.ExpirationTime = expirationTime;
             alarm.RecurrenceType = recurrenceType;

             if (action == null)
                 ScheduledActionService.Add(alarm);
             else
                 ScheduledActionService.Replace(alarm);
        }

        public static void RemoveAlarmOrReminder(string name)
        {
            ScheduledAction action = ScheduledActionService.Find(name);
            if (action != null)
                ScheduledActionService.Remove(action.Name);
        }
    }
}
