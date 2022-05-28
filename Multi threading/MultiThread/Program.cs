// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;
// sum of threads 
int SumOfThread = 0;
// file path storage variable
string Path = @"C:\Users\ulzii\source\repos\Multi threading\MultiThread\MyFile";
try
{
    if(!File.Exists(Path + 0 + ".txt"))
    {
        // Create new files     
        for (int index = 0; index < 40; index++)
        {
            int rand;
            using (BinaryWriter fs = new BinaryWriter(File.Create(Path + index + ".txt")))
            {
                Random random = new Random();
                for (int i = 0; i < 100000; i++)
                {
                    rand = random.Next(1, 9);
                    fs.Write(rand);
                }
                fs.Close();
            }
        }
        Console.WriteLine("File created.");
        //If a file is created, find the sum of the files
        Sum();
    }
    else
    {
        //If a file is be, find the sum of the files
        Console.WriteLine("File reading...");
        Sum();
    }
}
catch (Exception Ex)
{
    Console.WriteLine(Ex.ToString());
}
// Sum find of Files 
void Sum()
{
    if (File.Exists(Path + 0 + ".txt"))
    {
        // ThreadTime are objects that measure run time
        Stopwatch ThreadTime = new Stopwatch();
        // Create multi threads 
        Thread thread1 = new Thread(new ParameterizedThreadStart(MultiThread));
        Thread thread2 = new Thread(new ParameterizedThreadStart(MultiThread));
        Thread thread3 = new Thread(new ParameterizedThreadStart(MultiThread));
        Thread thread4 = new Thread(new ParameterizedThreadStart(MultiThread));
        //Create one thread
        Thread oneThread = new Thread(OneThread);

        object args1 = new object[2] { 0, 10 };
        object args2 = new object[2] { 10, 20 };
        object args3 = new object[2] { 20, 30 };
        object args4 = new object[2] { 30, 40 };
        // Launch ThreadTime
        ThreadTime.Start();
        //Threads runnig...
        thread1.Start(args1);
        thread2.Start(args2);
        thread3.Start(args3);
        thread4.Start(args4);
        while (true)
        {
            if (!thread1.IsAlive && !thread2.IsAlive && !thread3.IsAlive && !thread4.IsAlive)
            {
                Console.WriteLine("\n-----------------\n**MultiThread start...**");
                Console.WriteLine("Multithread sum = " + SumOfThread);
                //Stop ThreadTime
                ThreadTime.Stop();
                Console.WriteLine("Multithread Time is {0}", ThreadTime.ElapsedMilliseconds);
                break;
            }
        }
        //oneThread running...
        oneThread.Start();
    }
}
//Multi threads operation
void MultiThread(object args)
{
    Array arr = new object[2];
    arr = (Array)args;
    int sum = 0;
    int index = 0;
    try
    {
        //Read the meaning of files
        for (index = (int)arr.GetValue(0); index < (int)arr.GetValue(1); index++)
        {
            // Open the binaryReader and read it back.
            using (BinaryReader fs = new BinaryReader(File.OpenRead(Path + index + ".txt")))
            {
                try
                {
                    byte[] b = new byte[1024];
                    while (fs.Read(b, 0, b.Length) > 0)
                    {
                        for (int i = 0; i < b.Length; i++)
                        {
                            int a = (int)b[i];
                            sum = sum + a;
                        }
                    }
                }
                catch (EndOfStreamException e)
                {
                    Console.WriteLine(e.ToString());
                }
                //thread uud zereg duudagdan ajillahad hoyula zereg SumOfThread iin utgiig uurchluj bolohgui
                lock ((object)SumOfThread)
                {
                    SumOfThread += sum;
                }
                sum = 0;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}
//OneThread operation
void OneThread()
{
    // ThreadTime are objects that measure run time
    Stopwatch OneThreadTime = new Stopwatch();
    //Launch ThreadTime
    OneThreadTime.Start();
    Console.WriteLine("-----------------\n**OneThread start...**");
    int sum = 0;
    int index = 0;
    try
    {
        //Read the meaning of files
        for (index = 0; index < 40; index++)
        {
            // Open the binaryReader and read it back.
            using (BinaryReader fs = new BinaryReader(File.OpenRead(Path + index + ".txt")))
            {
                byte[] b = new byte[1024];
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    for (int i = 0; i < b.Length; i++)
                    {
                        int a = (int)b[i];
                        sum = sum + a;
                    }
                }
            }
        }
        Console.WriteLine("One Thread sum = " + sum);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }

    //Stop OneThreadTime
    OneThreadTime.Stop();
    Console.WriteLine("OneThread Time is {0}", OneThreadTime.ElapsedMilliseconds);
}