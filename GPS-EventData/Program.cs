// See https://aka.ms/new-console-template for more information
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using GPS_EventData;

byte[] rawData = File.ReadAllBytes(@"gpslog.gps");
int size;
int indexHead, indexTail;
byte[] head = new byte[1024];
byte[] tail = new byte[1024];
/// <summary>
/// GPS file nii head bolon tail  iin index iig oloh
/// </summary>
/*for (int i = 0; i < rawData.Length; i++)
{
    //int indexHead, indexTail;
    if (rawData[i] == 64)
    {
        if (rawData[i + 1] == 64)
        {
            if (rawData[i + 2] != 64)
            {
                indexHead = i;
                head = rawData;
                size = BitConverter.ToInt16(rawData[(i + 2)..(i + 4)]);

                Console.WriteLine("head index= " + indexHead + "-----" + BitConverter.ToInt16(rawData[i..(i + 2)]));
                Console.WriteLine("Packet size: " + size);
            }
        }
    }
    if (rawData[i] == 13)
        if (rawData[i + 1] == 10)
        {
            indexTail = i;
            tail = rawData;
            Console.WriteLine(" tail index= " + indexTail + "," + (indexTail + 1) + "-----" + BitConverter.ToInt16(rawData[i..(i + 2)]));
            Console.WriteLine("================================================================");
        }
}*/
/*
 *1.  [1..140] --> Real time
 * 2. [141..163] --> Event code is incorrect !!!
 * 3. [165..304] --> Real time
 * 4. [304..353] --> Sleep Mode
 * 5. [353..456] --> Historic 
 * 6. [457..560] --> Historic
 * 7. [560..564] -->
 */
try
{
    /*Packet a = new Packet(rawData[1..140]);
    new checkEventCode(a.eventCode, a.eventData);
    Packet b = new Packet(rawData[141..163]);
    new checkEventCode(b.eventCode, b.eventData);
    Packet c = new Packet(rawData[165..304]);
    new checkEventCode(c.eventCode, c.eventData);
    Packet d = new Packet(rawData[304..353]);
    new checkEventCode(d.eventCode, d.eventData);
    Packet e = new Packet(rawData[353..456]);
    new checkEventCode(e.eventCode, e.eventData);
    Packet f = new Packet(rawData[457..560]);
    new checkEventCode(f.eventCode, f.eventData);
    Packet g = new Packet(rawData[141..163]);
    new checkEventCode(g.eventCode, g.eventData);*/
    //Console.WriteLine(rawData.Length);
    Console.WriteLine(BitConverter.ToString(rawData[1..140]));
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}
/*
 *0x40, 0x40,0x56, 0x00, 0x32, 0x47, 0x51, 0x2d, 0x31, 0x36, 0x30, 0x31, 0x30, 0x36, 0x33, 0x38, 0x01, 0x10, 0x06,
  0x0a, 0x13,0x0a, 0x2a, 0x08, 0x0f, 0x3e, 0x40, 0x44, 0x0a, 0x70, 0x89, 0xe6, 0x16, 0xcf, 0x00, 0x50, 0x04, 0xd0,
  0x31, 0x01,0x01, 0x05, 0x05, 0x05, 0x00, 0x06, 0x00, 0x01, 0x01, 0x00, 0x00, 0x18, 0xa5, 0xa6, 0xa7, 0xa8, 0xa9,
  0xaa, 0xf2,0xad, 0xae, 0xaf, 0xb0, 0xb1, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6a, 0x6b, 0x6c,
 0x06, 0x0a,0x13, 0x0a, 0x2a, 0x07, 0x53, 0xe8, 0x0d, 0x0a
       
 */

