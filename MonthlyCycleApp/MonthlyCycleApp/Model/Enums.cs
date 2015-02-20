using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyCycleApp.Model
{
    public enum Forecast
    {
        // based on data given
        Standard = 1,
        //based on average
        Advanced = 2
    }


    public enum CaseType
    { 
        EarlyCycle=0,
        DuringCycleAsPredicted=1,
        DuringCycleEarlyEnd = 2,
        DuringCycleNormalEnd = 3,
        DuringPeriodLatePeriod =4,
        AfterPeriodLatePeriod =5,

    }

    public enum Timeframe
    {
        Before = 0,
        During = 1,
        After = 2
    }



    public enum Step
    {
        Unconfirmed = 0,
        Pending = 1,
        OnTime = 2,
        Early = 3,
        Late = 4,
        Delayed = 5,

        Show = 6
    }
}
