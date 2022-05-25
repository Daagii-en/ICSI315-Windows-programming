namespace GPS_EventData
{
    public class checkEventCode
    {
        public checkEventCode(uint eventCode, byte[] eventData)
        {
            switch (eventCode)
            {
                case 4097:
                    Login login = new Login(eventData);
                    break;
                case 4099:
                    //Maintenance(eventData);
                    Maintenance maintenance = new Maintenance(eventData);
                    break;
                case 8193:
                    //Realtime(eventData);
                    Realtime realtime = new Realtime(eventData);
                    break;
                case 8194:
                    //Historic(eventData);
                    Historic historic = new Historic(eventData);
                    break;
                case 8196:
                    //SleepMode(eventData);
                    SleepMode sleepMode = new SleepMode(eventData);
                    break;
                default:
                    Console.WriteLine("Event code is incorrect !!!");
                    break;
            }
        }
    }
}