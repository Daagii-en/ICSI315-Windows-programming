
using System.Text;

namespace GPS_EventData
{
    public class Login
    {
            //Declaring the attributes of the class : 

            //Consists of 6 bytes. Each will be divided to time infos 
            //platform status fixed or not
            public String status;
            //The device location (latitude)
            public double latitude;
            //The device location (longitude)
            public double longitude;
            //The machine speed
            public double speed;
            //The machine course
            public double course;
            //The device height when it sending signal
            public double high;
            public Login(byte[] eventData)
            {
            Console.WriteLine("-----Login check-----");
                //first 6 bytes of eventData will describe the time
                rtcTime(eventData[0..6]);
                //status is the device status and it will be described in string
                status = HexStringToBinary(BitConverter.ToString(eventData[6..7]));
                //We divided the byte section into bits to explain the numbers 
                statusDescription(status);
                //Location of the device is described in double data type for the accuracy 
                latitude = ((double)BitConverter.ToInt32(eventData[7..11]) / 3600000);
                Console.WriteLine(latitude);
                longitude = ((double)BitConverter.ToInt32(eventData[11..15]) / 3600000);
                Console.WriteLine(longitude);
                //Speed, course and high of the device is described in double data type for the accuracy
                speed = (BitConverter.ToInt16(eventData[15..17]) * 1.8);
                course = (BitConverter.ToInt16(eventData[17..19]) * 0.1);
                high = (BitConverter.ToInt16(eventData[19..21]) * 0.1);
                Console.WriteLine("Speed:" + speed + "     Course:" + course + "     High:" + high);
                //sending date time info
                rtcTime(eventData[(eventData.Length - 6)..(eventData.Length)]);

            }
            //Method that converts hex number to binary so we can describe binary numbers for the STATUS section.
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
            public void rtcTime(byte[] rtc_time)
            {
            int day = rtc_time[0];
            Console.WriteLine("Day : " + day);
            int month = rtc_time[1];
            Console.WriteLine("Month : " + month);
            String year = BitConverter.ToString(rtc_time[2..3]);
            Console.WriteLine("Year : 20" + year);
            int hour = rtc_time[3];
            Console.WriteLine("Hour : " + hour);
            int minute = rtc_time[4];
            Console.WriteLine("Minute : " + minute);
            int second = rtc_time[5];
            Console.WriteLine("Seconds : " + second);
        }
    }
}