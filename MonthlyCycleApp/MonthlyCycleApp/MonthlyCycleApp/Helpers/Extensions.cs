using MonthlyCycleApp.NotificationsAndTiles;
using MonthlyCycleApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyCycleApp.Helpers
{
    public static class Extensions
    {

        public static Dictionary<RecurencePeriodType, string> ToDictionary(this RecurencePeriodType recEnum)
        {
          

            Dictionary<RecurencePeriodType, string> dict = new Dictionary<RecurencePeriodType, string>();

            dict.Add(RecurencePeriodType.Yearly, AppResources.Yearly);
            dict.Add(RecurencePeriodType.SixMonths, AppResources.SixMonths);
            dict.Add(RecurencePeriodType.ThreeMonths, AppResources.ThreeMonths);
            dict.Add(RecurencePeriodType.Monthly, AppResources.Monthly);
            dict.Add(RecurencePeriodType.OneTime, AppResources.OneTime);
            return dict;
        }

        public static List<RecurencePeriod> ToList(RecurencePeriodType recEnum)
        {
            List<RecurencePeriod> list = new List<RecurencePeriod>();

            list.Add(new RecurencePeriod(RecurencePeriodType.Yearly, AppResources.Yearly, 12));
            list.Add(new RecurencePeriod(RecurencePeriodType.SixMonths, AppResources.SixMonths, 6));
            list.Add(new RecurencePeriod(RecurencePeriodType.ThreeMonths, AppResources.ThreeMonths, 3));
            list.Add(new RecurencePeriod(RecurencePeriodType.Monthly, AppResources.Monthly, 1));
            list.Add(new RecurencePeriod(RecurencePeriodType.OneTime, AppResources.OneTime, 0));
            return list;
        }


        public static Dictionary<RecurencePeriodType, Tuple<int, string>> ToTupleDictionary(RecurencePeriodType recEnum)
        {
            Dictionary<RecurencePeriodType, Tuple<int, string>> tuple = new Dictionary<RecurencePeriodType, Tuple<int, string>>();

            tuple.Add(RecurencePeriodType.Yearly,      new Tuple<int, string>(12, AppResources.Yearly));
            tuple.Add(RecurencePeriodType.SixMonths,   new Tuple<int, string>(6,  AppResources.SixMonths));
            tuple.Add(RecurencePeriodType.ThreeMonths, new Tuple<int, string>(3, AppResources.ThreeMonths));
            tuple.Add(RecurencePeriodType.Monthly,     new Tuple<int, string>(1, AppResources.Monthly));
            tuple.Add(RecurencePeriodType.OneTime,     new Tuple<int, string>(0, AppResources.OneTime));

            return tuple;
        }
    }
}
