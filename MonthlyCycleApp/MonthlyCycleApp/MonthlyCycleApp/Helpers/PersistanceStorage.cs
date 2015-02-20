using Monthly.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.Storage;
using WPControls.Models;
using WPControls.Helpers;


namespace Monthly.Helpers
{
    public static class PersistanceStorage
    {
        #region PeriodCalendar
        public static PeriodCalendar MockCalendar()
        {
            PeriodCalendar calendar = new PeriodCalendar();

            PeriodMonth september = new PeriodMonth(new DateTime(2014, 9, 1), 6, 28);
            PeriodMonth october = new PeriodMonth(new DateTime(2014, 9, 29), 6, 28);
            PeriodMonth november = new PeriodMonth(new DateTime(2014, 10, 27), 6, 28);
            PeriodMonth december = new PeriodMonth(new DateTime(2014, 11, 24), 6, 28);
            
            PeriodMonth january = new PeriodMonth(new DateTime(2015, 1, 19), 6, 28);
            PeriodMonth february = new PeriodMonth(new DateTime(2015, 2, 16), 6, 28);

            calendar.PastPeriods.Add(september);
            calendar.PastPeriods.Add(october);
            calendar.PastPeriods.Add(november);
            calendar.PastPeriods.Add(december);
            calendar.PastPeriods.Add(january);

            calendar.CurrentPeriod = february;

           // calendar.Year = 2014;
            return calendar;
        }

        private const string FILE_DIR = "DataFolder";
        private const string FILE_PATH = "DataFolder\\DataFile.txt";

        public static void WriteDataToPersistanceStorage(PeriodCalendar calendar)
        {
           // PeriodCalendar calendar = MockCalendar();
            IsolatedStorageFile local = IsolatedStorageFile.GetUserStoreForApplication();

            if (!local.DirectoryExists(FILE_DIR))
                local.CreateDirectory(FILE_DIR);

            using (var isoFileStream = new IsolatedStorageFileStream(FILE_PATH, FileMode.OpenOrCreate, local))
            {

               /* using (var memoryStream = new MemoryStream())
                {
                    BinarySerializationHelper.Serialize(memoryStream, calendar);
                    memoryStream.Position = 0;
                    memoryStream.CopyTo(isoFileStream);
                    memoryStream.Close();
                }
                * */

                DataContractSerializer ser = new DataContractSerializer(calendar.GetType());
                ser.WriteObject(isoFileStream, calendar);

                isoFileStream.Close();
            }
        }

        public static PeriodCalendar ReadDataFromPersistanceStorage()
        {
            IsolatedStorageFile local = IsolatedStorageFile.GetUserStoreForApplication();
            if (!local.DirectoryExists(FILE_DIR))
                return null;
            if (!local.FileExists(FILE_PATH))
                return null;

            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(FILE_PATH, FileMode.Open, local))
            {
                /*
                using (StreamReader streamReader = new StreamReader(isoStream))
                { */
                    PeriodCalendar calendar = new PeriodCalendar();

                    XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(isoStream, new XmlDictionaryReaderQuotas());
                    DataContractSerializer ser = new DataContractSerializer(typeof(PeriodCalendar));
                    calendar = (PeriodCalendar)ser.ReadObject(reader, true);
                    reader.Close();
                    isoStream.Close();


                    //    calendar = (PeriodCalendar)BinarySerializationHelper.Deserialize(streamReader.BaseStream, typeof(PeriodCalendar));
                    return calendar;
               // }
            }
        }
        #endregion

        #region CalendarObject

        public static CalendarObject MockCalendarObject()
        {
            CalendarObject calendar = new CalendarObject(2014);
           
            calendar.Months[0].SetPeriod(19, 6, 28);
            calendar.Months[1].SetPeriod(16, 6, 28);
            /*
            PeriodMonth september = new PeriodMonth(new DateTime(2014, 9, 1), 6, 28);
            PeriodMonth october = new PeriodMonth(new DateTime(2014, 9, 29), 6, 28);
            PeriodMonth november = new PeriodMonth(new DateTime(2014, 10, 27), 6, 28);
            PeriodMonth december = new PeriodMonth(new DateTime(2014, 11, 24), 6, 28);
            PeriodMonth january = new PeriodMonth(new DateTime(2014, 11, 19), 6, 28);
            PeriodMonth february = new PeriodMonth(new DateTime(2014, 11, 16), 6, 28);

            calendar.PastPeriods.Add(september);
            calendar.PastPeriods.Add(october);
            calendar.PastPeriods.Add(november);
            calendar.PastPeriods.Add(december);


            calendar.CurrentPeriod = january;
            */
            return calendar;
        }


        public static void WriteDataToPersistanceStorage(CalendarObject calendar)
        {
            // PeriodCalendar calendar = MockCalendar();
            IsolatedStorageFile local = IsolatedStorageFile.GetUserStoreForApplication();

            if (!local.DirectoryExists(FILE_DIR))
                local.CreateDirectory(FILE_DIR);

            using (var isoFileStream = new IsolatedStorageFileStream(FILE_PATH, FileMode.OpenOrCreate, local))
            {

                /* using (var memoryStream = new MemoryStream())
                 {
                     BinarySerializationHelper.Serialize(memoryStream, calendar);
                     memoryStream.Position = 0;
                     memoryStream.CopyTo(isoFileStream);
                     memoryStream.Close();
                 }
                 * */

                DataContractSerializer ser = new DataContractSerializer(calendar.GetType());
                ser.WriteObject(isoFileStream, calendar);

                isoFileStream.Close();
            }
        }

        public static CalendarObject ReadCalendarFromPersistanceStorage()
        {
            IsolatedStorageFile local = IsolatedStorageFile.GetUserStoreForApplication();
            if (!local.DirectoryExists(FILE_DIR))
                return null;
            if (!local.FileExists(FILE_PATH))
                return null;

            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(FILE_PATH, FileMode.Open, local))
            {
                /*
                using (StreamReader streamReader = new StreamReader(isoStream))
                { */
                CalendarObject calendar = new CalendarObject();

                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(isoStream, new XmlDictionaryReaderQuotas());
                DataContractSerializer ser = new DataContractSerializer(typeof(PeriodCalendar));
                calendar = (CalendarObject)ser.ReadObject(reader, true);
                reader.Close();
                isoStream.Close();


                //    calendar = (PeriodCalendar)BinarySerializationHelper.Deserialize(streamReader.BaseStream, typeof(PeriodCalendar));
                return calendar;
                // }
            }
        }

        #endregion
    }
}
