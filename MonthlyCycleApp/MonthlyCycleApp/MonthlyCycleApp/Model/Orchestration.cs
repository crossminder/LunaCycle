using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyCycleApp.Model
{
    public class Orchestration : IOrchestation
    {
       
        public override void Refresh(Timeframe momentInPeriod, Step startCycleStage, Step endCycleStage)
        {

           
        }
 /*
        public void Refresh(Timeframe momentInPeriod, Stage startCycleStage, Stage endCycleStage)
        {      
            switch(momentInPeriod)
            {
                case Timeframe.Before:
                    {
                        //invisible
                        //if Tap Refresh
                        break;
                    }
                case Timeframe.During:
                    {
                    switch (startCycleStage)
                    {
                        case Stage.OnTime:
                            {
                                 switch(endCycleStage)
                                {
                                    case Stage.OnTime:
                                        {
                                            //Refresh(duringCycle,confirmedCycleStartOnTime,confirmedCycleEndOnTime)
                                            break;
                                        }
                                    case Stage.Pending:
                                        {
                                            //NormalStartNormalEnd(duringCycle,confirmedCycleStartOnTime,pendingEndCycleConfirmation)
                                            break;
                                        }
                                    case Stage.Early:
                                        {
                                          //  NormalStartEarlyEnd(duringCycle,confirmedCycleStartOnTime, confirmedCycleEndBeforeTime, counter=no of days(diff) )
                                            break;
                                        }
                                    case Stage.Late:
                                        {
                                           // NormalStartLateEnd(duringCycle,confirmedCycleStartOnTime, confirmedCycleEndAtferTime, counter=  a cata zi de la normal end=day0 si 7 este pentru opacitate )
                                            break;
                                        }
                                }
                               break;
                            }
                        case Stage.Late:
                            {                 
                                switch(endCycleStage)
                                {
                                    case Stage.Unconfirmed:
                                        {
                                            //LateStart(duringCycle,notConfirmedStart) = show message
                                            break;
                                        }
                                    case Stage.Delayed:
                                        {
                                            //LateStartDelayed(duringCycle,delayedStart) = shows button
                                            break;
                                        }
                         

                                }
                                break;
                            }
                        case Stage.Pending:
                            {
                                switch(endCycleStage)
                                {
                                       case Stage.Unconfirmed:
                                        {
                                            //today<estimated initial end
                                           //LateStartDelayedWithinEstimation(duringCycle, pendingCycleStartLater,notConfirmedCycleEnd) - button pressed- show message to select start
                                            break;
                                        }
                                    case Stage.Pending:
                                        {
                                            //today>estimated initial end
                                            //LateStartDelayedOutsideEstimation(afterCycle,pendingCycleStartLater, pendingEndCycleConfirmation)- button pressed- show message to select start and end

                                            break;
                                        }
                                 }
                                break;
                            }
                    }
                                break;
                    }
                case Timeframe.After:
                    {
                      switch (startCycleStage)
                        {
                            case Stage.Late:
                                {
                                    switch(endCycleStage)
                                    {
                                      case Stage.Late:
                                        {
                                            //Refresh(afterCycle,confirmedCycleStartLater,confirmedCycleEndLater)
                                            break;
                                        }
                                    }
                                    break;
                                }
                          case Stage.Unconfirmed:
                              {
                                  switch(endCycleStage)
                                        {
                                          case Stage.Unconfirmed:
                                            {
                                                //Refresh(afterCycle,notConfirmedCycleStart,notConfirmedCycleEnd) - today>=28 days (cycle duration)
                                                break;
                                            }
                                        }
                                        break;
                              }
                          }
                        break;
                    }
            }
        }

        public void AdjustUI()
        {
            //set visibility on buttons

            //set visibility on screens

            // set opacity
        
        }




        */
    }
    
}
