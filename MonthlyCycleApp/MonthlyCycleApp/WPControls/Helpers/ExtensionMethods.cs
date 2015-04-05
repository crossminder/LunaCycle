using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPControls.Models;

namespace WPControls.Helpers
{
    public static class ExtensionMethods
    {
        public static bool IsEmpty(this PeriodMonth month)
        {
            return month.CycleDuration == 0 && month.PeriodDuration == 0;
        }

        public static bool IsNew(this PeriodCalendar calendar)
        {
            return calendar.PastPeriods == null && calendar.PastPeriods.Count == 0;
        }

        public static PeriodMonth FindOverlappingExistingPeriod(DateTime startDate, DateTime endDate, List<PeriodMonth> periods, PeriodMonth currentPeriod)
        {
            foreach (var item in periods)
            {
                DateRange addedPeriod = new DateRange(startDate, endDate);
                DateRange tempPeriod = new DateRange(item.PeriodStartDay, item.PeriodEndDay);
                if (addedPeriod.IsOverlapping(tempPeriod) && item != currentPeriod)
                    return item;
            }

            return null;
        }

        public static bool IsNew(this CalendarObject calendar)
        {
            return calendar.Months == null || calendar.Months == new List<MonthObject>();
        }


        public static void SetDayType(this CalendarItem calendarItem, PeriodCalendar calendar)
        {
            if (calendar != null && calendar.Periods != null)
            {
                PeriodMonth period = calendar.GetPeriodForDate(calendarItem.ItemDate);
                calendarItem.DayType = GetDayType(calendarItem.ItemDate, period);
            }
        }


        public static MonthObject GetMonthById(this List<MonthObject> months, int index)
        {
        return (from item in months 
                where item.MonthId==index
                    select item ).FirstOrDefault();
        }

        //public static PeriodDayTypeEnum GetDayTypeById(this List<MonthObject> months, int monthId, int dayId)
        //{
        //    MonthObject month = months.GetMonthById(monthId);
        //    return month != null && month.Days.Length > dayId - 1 ? month.Days[dayId - 1] : PeriodDayTypeEnum.RegularDay;
        //}

        public static PeriodDayTypeEnum GetDayType(DateTime date, PeriodMonth periodMonth)
        {
            //this means you found the right period and may keep on going

            if (periodMonth != null && date >= periodMonth.PeriodStartDay && date <= periodMonth.CycleEndDay)
            {
                if (date.Equals(periodMonth.PeriodStartDay))
                    return PeriodDayTypeEnum.CycleStartDay;

                if (date.Equals(periodMonth.PeriodEndDay))
                    return PeriodDayTypeEnum.CycleEndDay;

                if (date.Equals(periodMonth.FertilityStartDay))
                    return PeriodDayTypeEnum.FertilityStartDay;

                if (date.Equals(periodMonth.FertilityStartDay.AddDays(periodMonth.FertilityDuration - 1)))
                    return PeriodDayTypeEnum.FertilityEndDay;

                DateRange cycle = new DateRange(periodMonth.PeriodStartDay, periodMonth.PeriodEndDay);
                if (cycle.Includes(date))
                    return PeriodDayTypeEnum.CycleDay;

                if (date.Equals(periodMonth.OvulationDay))
                    return PeriodDayTypeEnum.OvulationDay;
                DateRange fertility = new DateRange(periodMonth.FertilityStartDay, periodMonth.FertilityStartDay.AddDays(periodMonth.FertilityDuration - 1));
                if (fertility.Includes(date))
                    return PeriodDayTypeEnum.FertilityDay;

            }
            return PeriodDayTypeEnum.RegularDay;
        }

        public static bool IsPillDay(DateTime date, PeriodMonth periodMonth)
        {
            return periodMonth!=null ? (date >= periodMonth.PeriodStartDay && date <= periodMonth.PeriodStartDay.AddDays(21)): false;
        }

        //public static void SetPeriod(this MonthObject month, int startPeriod, int periodDuration, int cycleDuration)
        //{
        //    month.Days = Enumerable.Repeat(PeriodDayTypeEnum.RegularDay, month.Days.Count()).ToArray();

        //    month.Days[startPeriod] = PeriodDayTypeEnum.CycleStartDay;
        //    for (int i = startPeriod; i < startPeriod + periodDuration; i++)
        //    {
        //        month.Days[i] = PeriodDayTypeEnum.CycleDay;
        //    }
        //   month.Days[startPeriod + periodDuration] = PeriodDayTypeEnum.CycleEndDay;
        //}
    }
 
    
}
