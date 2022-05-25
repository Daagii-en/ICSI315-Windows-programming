

using System.Diagnostics;

Multi_threading.MainThread makefile = new Multi_threading.MainThread();
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
string path = @"E:\repos\Multi threading\Multi threading\MyFile";
if (File.Exists(path + 0 + ".txt"))
{
    Thread multiThread1 = new Thread(new ParameterizedThreadStart(makefile.multiThread));
    Thread multiThread2 = new Thread(new ParameterizedThreadStart(makefile.multiThread));
    Thread multiThread3 = new Thread(new ParameterizedThreadStart(makefile.multiThread));
    Thread multiThread4 = new Thread(new ParameterizedThreadStart(makefile.multiThread));
    Thread oneThread = new Thread(makefile.oneThread);
    object args1 = new object[2] { 0, 10 };
    object args2 = new object[2] { 10, 20 };
    object args3 = new object[2] { 20, 30 };
    object args4 = new object[2] { 30, 40 };

    //
    multiThread1.Start(args1);
    multiThread2.Start(args2);
    multiThread3.Start(args3);
    multiThread4.Start(args4);

    oneThread.Start();

    Console.WriteLine("Multi Threads running...");
    // 4 thread iig zereg ajluulaad ur dung hevleh 
    if (!multiThread1.IsAlive && !multiThread2.IsAlive && !multiThread3.IsAlive && !multiThread4.IsAlive)
    {
        stopwatch.Stop();
        Console.WriteLine("Multithread sum = " + makefile.niilberInt);
        Console.WriteLine("Multi thread stop!!!");
        Console.WriteLine("Multithread Time =  {0} ms", stopwatch.ElapsedMilliseconds);


    }

}
else
{
    // Create a new file     
    for (int index = 0; index < 41; index++)
    {
        using (BinaryWriter fs = new BinaryWriter(File.Create(path + index + ".txt")))
        {
            Random random = new Random();
            for (int i = 0; i < 100000; i++)
            {
                fs.Write(1);
            }
            fs.Close();

        }
    }
    Console.WriteLine("File uusgej duuslaa");
}
Console.WriteLine("==========================================================");
//makefile.createFileReader();

if (!File.Exists(path))
{
    // Create the file.
    using (FileStream fs = File.Create(path))
    {
        String[] num = new string[100000];
        byte[] data = new byte[100000];
        // Add some information to the file.
        Random random = new Random();
        for(int i = 0; i < num.Length; i++)
        {
            num[i] = random.NextInt64(0,9).ToString();
            fs.Write(data,(int)num[i],9);
        }
        for (int i = 0; i < 100000; i++)
        {
            //fs.Write();//new Random().Next
        }
        fs.Close();
        //fs.Write(num, 0, num.Length);
    }
}