using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyCycleApp.Model
{
   public abstract class IOrchestation
    {
       public abstract void Refresh(Timeframe momentInPeriod, Step startCycleStage, Step endCycleStage);
    }

   


}
