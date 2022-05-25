using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS_EventData
{
    /// <summary>
    /// SleepMode class нь RTC-TIME болон GPS-INFO data-г задлан шалгана.
    /// </summary>
    public class SleepMode
    {
        /// <summary>
        /// SleepMode class-ийн field үүд
        /// </summary>
       // public byte[] rtc_time;//UTC_TIME
        public String status;
        public double latitude;
        public double longitude;
        public double speed;
        public double course;
        public double high;
        /// <summary>
        /// SleepMode class-ийн constructor function
        /// </summary>
        /// <param name="eventData"></param>
        public SleepMode(byte[] eventData)
        {
            try
            {
                Console.WriteLine("--->SleepMode check<---");
                //
                rtc_time(eventData[0..6]);
                //
                gps_data(eventData[6..21]);
            }catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// RTC-TIME -г задлах function
        /// </summary>
        /// <param name="rtc_time"></param>
        public void rtc_time(byte[] rtc_time)
        {
            
            if (rtc_time[0] >= 31 || rtc_time[0] <= 0)
            {
                throw new Exception("Day error!!!");
            }
            int day = rtc_time[0];
            Console.WriteLine("Day : " + day);
            if (rtc_time[1] >= 13 || rtc_time[1] <= 0)
            {
                throw new Exception("Month error!!!");
            }
            int month = rtc_time[1];
            Console.WriteLine("Month : " + month);
            String year = BitConverter.ToString(rtc_time[2..3]);
            Console.WriteLine("Year : 20" + year);
            if (rtc_time[3] >= 25 || rtc_time[3] <= 0)
            {
                throw new Exception("Hour error");
            }
            int hour = rtc_time[3];
            Console.WriteLine("Hour : " + hour);
            if (rtc_time[4] >= 60 || rtc_time[4] <= -1)
            {
                throw new Exception("Minute error!!!");
            }
            int minute = rtc_time[4];
            Console.WriteLine("Minute : " + minute);
            if (rtc_time[5] >= 60 || rtc_time[5] <= -1)
            {
                throw new Exception("Second error!!!");
            }
            int second = rtc_time[5];
            Console.WriteLine("Seconds : " + second);
        }

        /// <summary>
        /// GPS_INFO-г задлах function
        /// </summary>
        /// <param name="eventData"></param>
        public void gps_data(byte[] eventData)
        {
            //rtc_time(eventData[0..6]);
            Console.WriteLine("--->GPS_INFO data<---");
            status = HexStringToBinary(BitConverter.ToString(eventData[0..1]));
            statusDescription(status);
            latitude = ((double)BitConverter.ToInt32(eventData[1..5]) / 3600000);
            Console.WriteLine("latitude: "+latitude);
            longitude = ((double)BitConverter.ToInt32(eventData[5..9]) / 3600000);
            Console.WriteLine("longitude: "+longitude);
            speed = (BitConverter.ToInt16(eventData[9..11]) * 1.8);
            course = (BitConverter.ToInt16(eventData[11..13]) * 0.1);
            high = (BitConverter.ToInt16(eventData[13..15]) * 0.1);
            Console.WriteLine("-Speed:" + speed + "     -Course:" + course + "     -High:" + high);

        }
        /// <summary>
        /// 16 тын тэмдэгтүүдийн 2 тын тэмдэглээг хадгалсан Object
        /// </summary>
        private static readonly Dictionary<char, string> hexCharacterToBinary = new Dictionary<char, string> {
                    { '0', "0000" },
                    { '1', "0001" },
                    { '2', "0010" },
                    { '3', "0011" },
                    { '4', "0100" },
                    { '5', "0101" },
                    { '6', "0110" },
                    { '7', "0111" },
                    { '8', "1000" },
                    { '9', "1001" },
                    { 'a', "1010" },
                    { 'b', "1011" },
                    { 'c', "1100" },
                    { 'd', "1101" },
                    { 'e', "1110" },
                    { 'f', "1111" }
            };
        /// <summary>
        /// String ийн 16 тын char тэмдэгтийг binary болгон буцаах method 
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public string HexStringToBinary(string hex)
        {

            StringBuilder result = new StringBuilder();
            foreach (char c in hex)
            {
                result.Append(hexCharacterToBinary[char.ToLower(c)]);
            }
            return result.ToString();
        }
        /// <summary>
        /// packet ийн map ийн status ийг задлах.
        /// </summary>
        /// <param name="a"></param>
        public void statusDescription(String a)
        {
            if (a[1] == 0) Console.WriteLine("Location : not fixed");
            else Console.WriteLine("Location : fixed");
            if (a[2] == 0) Console.WriteLine("North latitude");
            else Console.WriteLine("South latitude");
            if (a[3] == 0) Console.WriteLine("East longitude");
            else Console.WriteLine("West longitude");
        }
    }
}
