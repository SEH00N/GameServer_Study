using System.Globalization;

namespace Thread1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Thread th = new Thread(new ThreadStart(Run));
            ////ThreadStart thStart = new ThreadStart(Run);
            ////Thread th = new Thread(thStart);

            //th.Start();

            //int i = 5;
            //Thread th2 = new Thread(new ParameterizedThreadStart(Parameterized_Run2));
            //th2.Start(i);

            //--------------------------------------------------

            //Thread th1 = new Thread(new ThreadStart(Func1));
            //Thread th2 = new Thread(new ThreadStart(Func2));
            //th1.Start();
            //th2.Start();
            //Console.WriteLine("메인 종료");
            //Console.ReadLine();

            //--------------------------------------------------

            //Thread th = new Thread(new ThreadStart(Func));
            //th.IsBackground = true;
            //th.Start();
            //Thread.Sleep(3000);
            //Console.WriteLine("Main 종료");

            //--------------------------------------------------

            //Thread th = new Thread(new ThreadStart(FuncJoin));
            //th.Start();
            //th.Join();
            //Console.WriteLine("Main");

            //--------------------------------------------------

            Thread th = new Thread(new ThreadStart(FuncInterrupt1));
            th.Start();
            Console.WriteLine($"Main 스레드 {Thread.CurrentThread.GetHashCode()}");
            Console.WriteLine("Main 종료");
        }

        static void FuncInterrupt1()
        {
            Console.WriteLine($"FuncInterrrupt1 스레드 {Thread.CurrentThread.GetHashCode()}");
            Thread th = new Thread(new ThreadStart(FuncInterrupt2));
            th.Start();

            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine(i * 10);
                Thread.Sleep(200);

                if(i == 3)
                {
                    Console.WriteLine("FuncInterrupt1 종료");
                    Thread.CurrentThread.Interrupt();
                    //return;
                }
            }
        }

        static void FuncInterrupt2()
        {
            Console.WriteLine($"FuncInterrupt2 스레드 {Thread.CurrentThread.GetHashCode()}");
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(200);
            }

            Console.WriteLine("FuncInterrupt2 종료");
        }

        static void FuncJoin()
        {
            for(int i = 0; i < 30; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(200);
            }
        }

        static void Func()
        {
            int i = 0;
            while(true)
            {
                Console.WriteLine(i++);
                Thread.Sleep(300);
            }
        }

        static void Func1()
        {
            for(int i = 0; i  < 10; i ++)
                Console.WriteLine($"스레드1 호출 {i}");
        }
        static void Func2()
        {

            for(int i = 0; i  < 10; i ++)
                Console.WriteLine($"스레드2 호출 {i}");
        }

        static void Run()
        {
            Console.WriteLine("스레드에서 호출");
        }

        static void Parameterized_Run2(object obj)
        {
            for(int i = 0; i < (int)obj; i++)
                Console.WriteLine($"Parameterized 스레드에서 호출 : {i}");
        }
    }
}