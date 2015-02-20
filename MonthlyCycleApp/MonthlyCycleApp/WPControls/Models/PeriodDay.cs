using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WPControls.Helpers;


namespace WPControls.Models
{
    public class PeriodDay
    {
        #region Private
   
        private static Dictionary<PeriodDayTypeEnum, SolidColorBrush> color = new Dictionary<PeriodDayTypeEnum, SolidColorBrush>();
        private static List<Tuple<int, Point, SolidColorBrush>> details = new List<Tuple<int, Point, SolidColorBrush>>()
        {
          new Tuple<int, Point, SolidColorBrush>(
               (int)Convert.ChangeType(PeriodDayTypeEnum.CycleDay, typeof(Int32)),
                new Point(85, 310),
                new SolidColorBrush(Color.FromArgb(255, 255, 255, 255))
                ),
           new Tuple<int, Point, SolidColorBrush>(
              (int)Convert.ChangeType(PeriodDayTypeEnum.CycleStartDay, typeof(Int32)),
               new Point(85, 310),
               new SolidColorBrush(Color.FromArgb(255, 255, 255, 255))
               ),
               new Tuple<int, Point, SolidColorBrush>(
              (int)Convert.ChangeType(PeriodDayTypeEnum.CycleEndDay, typeof(Int32)),
               new Point(85, 310),
               new SolidColorBrush(Color.FromArgb(255, 255, 255, 255))
               ),
               new Tuple<int, Point, SolidColorBrush>(
              (int)Convert.ChangeType(PeriodDayTypeEnum.FertilityDay, typeof(Int32)),
               new Point(85, 310),
               new SolidColorBrush(Color.FromArgb(255, 255, 255, 255))
               ),
               new Tuple<int, Point, SolidColorBrush>(
              (int)Convert.ChangeType(PeriodDayTypeEnum.FertilityStartDay, typeof(Int32)),
               new Point(85, 310),
               new SolidColorBrush(Color.FromArgb(255, 255, 255, 255))
               ),
               new Tuple<int, Point, SolidColorBrush>(
              (int)Convert.ChangeType(PeriodDayTypeEnum.FertilityEndDay, typeof(Int32)),
               new Point(85, 310),
               new SolidColorBrush(Color.FromArgb(255, 255, 255, 255))
               ),
               new Tuple<int, Point, SolidColorBrush>(
              (int)Convert.ChangeType(PeriodDayTypeEnum.RegularDay, typeof(Int32)),
               new Point(235, 238),
               new SolidColorBrush(Color.FromArgb(255, 255, 255, 255))
               )
    };
        #endregion

        public PeriodDay(PeriodMonth period)
        {
            this.Type = ExtensionMethods.GetDayType(this.Day, period);
            this.CanTakePill = ExtensionMethods.IsPillDay(this.Day, period);
            int index = (int)Convert.ChangeType(Type, typeof(Int32));
            this.Location = details[index].Item2;
            this.BackgroundColor = details[index].Item3;
        }

        public DateTime Day { get { return DateTime.Today; } }

        public PeriodDayTypeEnum Type
        {
            get;
            set;
        }

        public Point Location
        {
            get;
            set;
        }

        public SolidColorBrush BackgroundColor
        {
            get;
            set;
        }

        public bool CanTakePill
        {
            get;
            set;
        }
    }
}
