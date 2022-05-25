using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS_EventData
{
    /// <summary>
    /// Historic class 
    /// </summary>
    public class Historic
    {
        /// <summary>
        ///historic class-ийн Field үүд
        /// </summary>
        //public byte[] rtc_time;//UTC_TIME  6
        //public byte[] data_switch;  // 3
        //public byte[] gps_data;//GPS_INFO  21
       // public byte[] obd_data ;// <=55
        public ushort trip_fuel; //4
        public ushort trip_mileage; //4
        public ushort trip_duration; // 4
        uint obdLength;
        private string status;
        private double latitude;
        private double longitude;
        private double speed;
        private double course;
        private double high;

        /// <summary>
        /// rawPacket-ийн eventData-ний historic data-г задлах class
        /// </summary>
        /// <param name="eventData"></param>
        public Historic(byte[] eventData)
        {
            Console.WriteLine("-----Historic check-----");
            //rtc_time iig zadlah
            rtc_time( eventData[0..6]);
            // dataSwitch iig zadlah
            dataSwitch(eventData[6..9]);
            //gps_data zadlah
            gps_data(eventData[9..30]);
            //obd data zadlah
            obd_data(eventData[30..(eventData.Length-12)]);
            //-------------------------------------------------//
            trip_fuel = BitConverter.ToUInt16(eventData[(eventData.Length - 12)..(eventData.Length - 8)]);
            Console.WriteLine("Trip_fuel:" + trip_fuel + " L");
            trip_mileage = BitConverter.ToUInt16(eventData[(eventData.Length-8)..(eventData.Length-4)]);
            Console.WriteLine("Trip_mileage" + trip_mileage + " Meter");
            trip_duration = BitConverter.ToUInt16(eventData[(eventData.Length-4)..(eventData.Length)]);
            Console.WriteLine("Trip_duration" + trip_duration + " MS");
        }
        /// <summary>
        /// RTC-TIME -г задлах function
        /// </summary>
        /// <param name="rtc_time"></param>
        public void rtc_time(byte[] rtc_time)
        {
            
            if (rtc_time[0] >= 31 || rtc_time[0] <= 0 )
            {
                throw new Exception("Day error!!!");
            }
            int day = rtc_time[0];
            Console.WriteLine("Day : " + day);
            if(rtc_time[1] >= 13 || rtc_time[1] <= 0)
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
            if(rtc_time[4]>=60 || rtc_time[4] <= -1)
            {
                throw new Exception("Minute error!!!");
            }
            int minute = rtc_time[4];
            Console.WriteLine("Minute : " + minute);
            if( rtc_time[5]>=60 || rtc_time[5] <= -1)
            {
                throw new Exception("Second error!!!");
            }
            int second =rtc_time[5];
            Console.WriteLine("Seconds : " + second);
        }

        /// <summary>
        /// Data switch -ийг задлах function
        /// </summary>
        /// <param name="data_switch"></param>
        public void dataSwitch(byte[] data_switch)
        {
            if (data_switch[0] == 00)Console.WriteLine("0 No GPS Data.");
            else if(data_switch[0] == 80) Console.WriteLine(" 80 With GPS Data");
            if (data_switch[1] == 00) Console.WriteLine("0 No OBD Data.");
            else if(data_switch[1] == 80) Console.WriteLine("80 With OBD Data");
            if (data_switch[2] == 00) Console.WriteLine("0 No GSERSOR Data.");
            else if(data_switch[2] == 80) Console.WriteLine("With GSERSOR Data");

        }
        /// <summary>
        /// GPS_INFO-г задлах function
        /// </summary>
        /// <param name="eventData"></param>
        public void gps_data(byte[] eventData)
        {
            rtc_time(eventData[0..6]);
            Console.WriteLine("GPS_INFO data");
            status = HexStringToBinary(BitConverter.ToString(eventData[6..7]));
            statusDescription(status);
            latitude = ((double)BitConverter.ToInt32(eventData[7..11]) / 3600000);
            Console.WriteLine(latitude);
            longitude = ((double)BitConverter.ToInt32(eventData[11..15]) / 3600000);
            Console.WriteLine(longitude);
            speed = (BitConverter.ToInt16(eventData[15..17]) * 1.8);
            course = (BitConverter.ToInt16(eventData[17..19]) * 0.1);
            high = (BitConverter.ToInt16(eventData[19..21]) * 0.1);
            Console.WriteLine("Speed:" + speed + "     Course:" + course + "     High:" + high);
        }
        /// <summary>
        /// OBD_DATA-г задлах function
        /// </summary>
        /// <param name="obd_data"></param>
        public void obd_data(byte[] eventData)
        {
            Console.WriteLine("-------OBD-------");
            obdLength = eventData[0];
            if (obdLength <= 10)
            {
                Console.WriteLine("OBD data has " + obdLength + " amount of data");
                /*obd data is described in this loop
                 * loop repeated as numbers of obd data 
                */
                for (int i = 1; i <= obdLength; i++)
                {
                    int a = BitConverter.ToInt16(eventData[(1)..(3)]);
                    //Console.WriteLine("obd data nii ymar neg ym: " + a);
                    int s = eventData[3];
                    for (int j = 0; j < s; j++)
                    {
                        int b = eventData[3 + j];
                        //Console.WriteLine("3 index ees hoish byte uud: " + b);
                    }

                }
            }
            //if amount of obd data is more than 10
            else Console.WriteLine("OBD data length is incorrect");
            //fuel info

        }

        /// <summary>
        /// 
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
        /// 
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
        /// GPS_INFO nii status iig tailbarlah function
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
