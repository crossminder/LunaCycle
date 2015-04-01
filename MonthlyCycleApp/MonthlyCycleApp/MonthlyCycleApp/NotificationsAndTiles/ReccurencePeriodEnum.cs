using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MonthlyCycleApp.NotificationsAndTiles
{
    public enum RecurencePeriodType
    {
        Yearly = 0,
        SixMonths = 1,
        ThreeMonths=2,
        Monthly = 3,
        OneTime = 4,

    }

    public class RecurencePeriod
    {
        public string ReccurenceName { get; set; }

        public RecurencePeriodType ReccurenceType { get; set; }

        public int ReccurenceValue { get; set; }

        public RecurencePeriod(RecurencePeriodType type, string name, int value)
        {
            ReccurenceType = type;
            ReccurenceName = name;
            ReccurenceValue = value;
        }
    }

  
}
