using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPControls.Models
{
    public enum PeriodDayTypeEnum
    {
        CycleStartDay = 0,
        CycleDay = 1,
        CycleEndDay = 2,
        FertilityStartDay = 3,
        FertilityDay = 4,
        FertilityEndDay = 5,
        RegularDay = 6,

        NotAssigned = -1,
    }

}
