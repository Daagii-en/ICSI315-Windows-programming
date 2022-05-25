using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Multi_threading;

using System.Diagnostics;
using System.Collections;

namespace Multi_threading
{
    public class MainThread
    {
        public int niilberInt = 0;
        /*public MainThread()
        {
            
        }*/
        public void multiThread( Object? param)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //thread iin hugataag hemjih object
            Array arr = new object[2];
            arr = (Array)param;
            int sum = 0;
            int index = 0;
            //file iin zamiig zaan
            string FileName = @"E:\repos\Multi threading\Multi threading\MyFile";
            for (index = (int)arr.GetValue(0); index < (int)arr.GetValue(1); index++)
            {
                //file iig open hiij br d hiij uguh
                using (BinaryReader br = new BinaryReader(File.OpenRead(FileName + index + ".txt")))
                {
                    //br iin utga buriig a-d onoogood 
                    int a;
                    while ((a = br.ReadInt32()) != null)
                    {
                        sum = sum + a;
                    }
                    /*for(int i = 0; i < br.ReadInt32(); i++)
                    {
                        a = br.ReadInt32();
                        sum = sum + a;
                    }*/
                    //niilberInt = niilberInt + sum;

                    lock ((object)niilberInt)
                    {
                        niilberInt = niilberInt + sum;
                    }
                    sum = 0;
                }
            }
           // Console.WriteLine("Multithread "+ param+ "sum = " + niilberInt);
            stopwatch.Stop();
            //Console.WriteLine("Multithread Time =  {0} ms", stopwatch.ElapsedMilliseconds);
        }
        public void oneThread()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("OneThread running...");
            //niilber hadgalah sum field
            int sum = 0;
            string filename = @"E:\repos\Multi threading\Multi threading\MyFile";
            //uusgesen 40 file iig unshih
            for (int i = 0; i < 41; i++)
            {
                //file iig open hiigeed  br -d onoogooh 
                using (BinaryReader br = new BinaryReader(File.OpenRead(filename + i + ".txt")))
                {
                    int a;
                    while ((a = br.ReadInt32()) != null)
                    {
                        sum = sum + a;
                    }

                }
            }
            Console.WriteLine("The sum of one thread worked: " + sum);
            stopwatch.Stop();

            Console.WriteLine("One thread Time = {0} ms", stopwatch.ElapsedMilliseconds);
        }
        /// <summary>
        /// create the files  
        /// </summary>
        public void createFileReader()
        {
            //
            string fileName = @"E:\repos\Multi threading\Multi threading\MyFile";

            Console.WriteLine("Threads running...");
            //thread iin hugataag hemjih object
            Stopwatch stopwatch = new Stopwatch();
            try
            {
                //If a file exists, create threads and find the sum of the numbers in the file
                if (File.Exists(fileName + 0 + ".txt"))
                {
                    //File.Delete(fileName);
                    Thread thread1 = new Thread(new ParameterizedThreadStart(Work));
                    Thread thread2 = new Thread(new ParameterizedThreadStart(Work));
                    Thread thread3 = new Thread(new ParameterizedThreadStart(Work));
                    Thread thread4 = new Thread(new ParameterizedThreadStart(Work));
                    //
                    Thread oneThread = new Thread(Work2);
                    //
                    object args1 = new object[2] { 0, 10 };
                    object args2 = new object[2] { 10, 20 };
                    object args3 = new object[2] { 20, 30 };
                    object args4 = new object[2] { 30, 40 };
                    //
                    stopwatch.Start();
                    //
                    thread1.Start(args1);
                    thread2.Start(args2);
                    thread3.Start(args3);
                    thread4.Start(args4);
                    //
                    oneThread.Start();

                    while (true)
                    {
                        // 4 thread iig zereg ajluulaad ur dung hevleh 
                        if (!thread1.IsAlive && !thread2.IsAlive && !thread3.IsAlive && !thread4.IsAlive)
                        {
                            stopwatch.Stop();
                            Console.WriteLine("Multithread Time is {0} ms", stopwatch.ElapsedMilliseconds);
                            Console.WriteLine("Multithread: " + niilberInt);
                            break;
                        }
                    }

                }
                else
                {
                    // Create a new file     
                    for (int index = 0; index < 40; index++)
                    {
                        using (BinaryWriter fs = new BinaryWriter(File.Create(fileName + index + ".txt")))
                        {
                            Random random = new Random();
                            for (int i = 0; i < 100000; i++)
                            {
                                fs.Write(1);//new Random().Next
                            }
                            fs.Close();

                        }
                    }
                    Console.WriteLine("File uusgej duuslaa");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// Work function ashiglan file dotor baigaa toonuudiin niilber oloh
        /// </summary>
        /// <param name="param"></param>
        public void Work(Object? param)
        {
            Array arr = new object[2];
            arr = (Array)param;
            int sum = 0;
            int index = 0;
            try
            {
                //file iin zamiig zaan
                string FileName = @"E:\repos\Multi threading\Multi threading\MyFile";

                for (index = (int)arr.GetValue(0); index < (int)arr.GetValue(1); index++)
                {
                    //file iig open hiij br d hiij uguh
                    using (BinaryReader br = new BinaryReader(File.OpenRead(FileName + index + ".txt")))
                    {
                        try
                        {
                            //br iin utga buriig a-d onoogood 
                            int a;
                            while ((a = br.ReadInt32()) != null)
                            {
                                //a -g sum -d nemegduulj ugnu
                                sum = sum + a;
                            }
                        }
                        catch (EndOfStreamException ex)
                        {

                        }


                        lock ((object)niilberInt)
                        {
                            niilberInt = niilberInt + sum;
                        }
                        sum = 0;
                    }
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// One thread iin ajilgaag shalgah function
        /// </summary>
        public void Work2()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("OneThread running...");
            //niilber hadgalah sum field
            int sum = 0;
            int i = 0;
            try
            {
                string filename = @"E:\repos\Multi threading\Multi threading\MyFile";
                //uusgesen 40 file iig unshih
                for (i = 0; i < 40; i++)
                {
                    //file iig open hiigeed  br -d onoogooh 
                    using (BinaryReader br = new BinaryReader(File.OpenRead(filename + i + ".txt")))
                    {
                        try
                        {
                            int a;
                            while ((a = br.ReadInt32()) != null)
                            {
                                sum = sum + a;
                            }
                        }
                        catch (EndOfStreamException ex)
                        {

                        }
                    }
                }
                Console.WriteLine("The sum of one thread worked: " + sum);
                stopwatch.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("Time of one thread worked: {0} ms", stopwatch.ElapsedMilliseconds);
        }
    }
}
