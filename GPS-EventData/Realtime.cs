using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS_EventData
{
    public class Realtime
    {
        //byte[] report = new byte[2];
        //byte[] rtcTime = new byte[6];
        //byte[] dataSwtich = new byte[3];
        //byte[] gpsData = new byte[21];
        //obd data length
        byte obdLength;
        //fuel 
        uint fuel;
        //trip milleage will be saved in tripMill
        uint tripMill;
        //trip duration
        uint duration;
        public ushort trip_fuel; //4
        public ushort trip_mileage; //4
        public ushort trip_duration; // 4
        public Realtime(byte[] eventData)
        {
            Console.WriteLine("-----Realtime check-----");
            //rtc method is for describing infos about time
            rtcTime(eventData[0..6]);
            //dataSwitch method is to explain data about if the package got sensor or other datas
            dataSwitch(eventData[6..9]);
            //gpsData provides infos such time, altitude and speed
            gpsData(eventData[9..30]);
            //obd data length will be saved in this variable
            
            obd_data(eventData[30..(eventData.Length - 12)]);
            //-------------------------------------------------//
            trip_fuel = BitConverter.ToUInt16(eventData[(eventData.Length - 12)..(eventData.Length - 8)]);
            Console.WriteLine("Trip_fuel: " + trip_fuel + " L");
            trip_mileage = BitConverter.ToUInt16(eventData[(eventData.Length - 8)..(eventData.Length - 4)]);
            Console.WriteLine("Trip_mileage: " + trip_mileage + " Meter");
            trip_duration = BitConverter.ToUInt16(eventData[(eventData.Length - 4)..(eventData.Length)]);
            Console.WriteLine("Trip_duration: " + trip_duration + " MS");
            //j is declared.(index)
           
        }
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
                    //Console.WriteLine("obd data nii ymar neg ym: "+a);
                    int s = eventData[3];
                    for(int j = 0; j < s; j++)
                    {
                        int b = eventData[3 + j];
                       // Console.WriteLine("3 index ees hoish byte uud: " + b);
                    }
                      
                }
            }
            //if amount of obd data is more than 10
            else Console.WriteLine("OBD data length is incorrect");
            
        }
        public void rtcTime(byte[] rtc_time)
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
        public void dataSwitch(byte[] a)
        {
            if (a[0] == 128) Console.WriteLine("The package has GPS data");
            else Console.WriteLine("No GPS data");

            if (a[1] == 128) Console.WriteLine("The package has OBD data");
            else Console.WriteLine("No OBD data");

            if (a[2] == 128) Console.WriteLine("The package has Sensor data");
            else Console.WriteLine("No Sensor data");
        }
        public void gpsData(byte[] a)
        {
            //day will be first byte
            int day = int.Parse(BitConverter.ToString(a[0..1]));
            Console.WriteLine("Day : " + day);
            //month will be second two bytes
            int month = int.Parse(BitConverter.ToString(a[1..2]), System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine("Month : " + month);
            //year is the 2-3 byte of evendData section
            String year = BitConverter.ToString(a[2..3]);
            Console.WriteLine("Year : 20" + year);
            //hour is the 3 - 4 byte of the utctime
            int hour = int.Parse(BitConverter.ToString(a[3..4]), System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine("Hour : " + hour);
            //minute described in utcTime 4-5 byte
            int minute = int.Parse(BitConverter.ToString(a[4..5]), System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine("Minute : " + minute);
            //seconds 5-6 byte of the utcTime
            int second = int.Parse(BitConverter.ToString(a[5..6]), System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine("Seconds : " + second);
            //status is the device status and it will be described in string
            String status = HexStringToBinary(BitConverter.ToString(a[6..7]));
            //We divided the byte section into bits to explain the numbers 
            statusDescription(status);
            //Location of the device is described in double data type for the accuracy 
            double latitude = ((double)BitConverter.ToInt32(a[7..11]) / 3600000);
            Console.WriteLine(latitude);
            double longitude = ((double)BitConverter.ToInt32(a[11..15]) / 3600000);
            Console.WriteLine(longitude);
            //Speed, course and high of the device is described in double data type for the accuracy
            double speed = (BitConverter.ToInt16(a[15..17]) * 1.8);
            double course = (BitConverter.ToInt16(a[17..19]) * 0.1);
            double high = (BitConverter.ToInt16(a[19..21]) * 0.1);
            Console.WriteLine("Speed:" + speed + "     Course:" + course + "     High:" + high);
        }
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

        //Method that converts hex number to binary so we can describe binary numbers for the STATUS section.
        public string HexStringToBinary(string hex)
        {

            StringBuilder result = new StringBuilder();
            foreach (char c in hex)
            {
                result.Append(hexCharacterToBinary[char.ToLower(c)]);
            }
            return result.ToString();
        }
        //Method explain status separetaly 
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
