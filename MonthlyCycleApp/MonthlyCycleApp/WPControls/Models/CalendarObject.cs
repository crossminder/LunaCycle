using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WPControls.Models
{
    [DataContract]
    public class CalendarObject
    {
        [DataMember]
        public List<MonthObject> Months { get; set; }

       

        public CalendarObject()
        {
            Months = new List<MonthObject>();
        }

        public CalendarObject(int year)
        {
           // Year = year;
            Months = new List<MonthObject>();
            //january
            Months.Add(new MonthObject(year, 1, 31));
            //february
            Months.Add(new MonthObject(year,2, DateTime.DaysInMonth(year, 2)));
            //march
            Months.Add(new MonthObject(year, 3, 31));
            //april
            Months.Add(new MonthObject(year, 4, 30));
            //may
            Months.Add(new MonthObject(year, 5, 31));
            //june
            Months.Add(new MonthObject(year, 6, 30));
            //july
            Months.Add(new MonthObject(year, 7, 31));
            //august
            Months.Add(new MonthObject(year, 8, 31));
            //september
            Months.Add(new MonthObject(year, 9, 30));
            //october
            Months.Add(new MonthObject(year, 10, 31));
            //november
            Months.Add(new MonthObject(year, 11, 30));
            //december
            Months.Add(new MonthObject(year, 12, 31));
        }
    }

    [DataContract]
    public class MonthObject
    {
         [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int MonthId { get; set; }

        [DataMember]
        public PeriodDayTypeEnum[] Days { get; set; }

        public DateTime PeriodStart
        {
            get
            {
                return Days.ToList().Contains(PeriodDayTypeEnum.CycleStartDay)?
                    new DateTime(Year, MonthId, Days.ToList().IndexOf(PeriodDayTypeEnum.CycleStartDay) +1) : DateTime.MinValue ;
            }
        }

        public MonthObject(int year, int monthId, int daysInMonth)
        {
            Year = year;
            MonthId = monthId;
            Days = new PeriodDayTypeEnum[daysInMonth+1];
          
            //default initialization
            Days = Enumerable.Repeat(PeriodDayTypeEnum.NotAssigned, daysInMonth).ToArray();          
        }
       
    }
}
