using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS_EventData
{
    public class Maintenance
    {

        /*Role of the class is basically showing the signals of the device just like a ping
         * So it will print that string to let us know that pings are coming in the package
         */
        public Maintenance(byte[] eventCode)
        {
            Console.WriteLine("Maintenance check");
            Console.WriteLine("The unit is on.");

        }
    }
}
