namespace Thread3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num = 0;
            object obj = new object();
            Thread t1 = new Thread(() => {
                for (int i = 0; i < 100000000; i++)
                {
                    lock (obj)
                        num++;
                    //try
                    //{
                    //    Monitor.Enter(obj);
                    //    num++;
                    //}
                    //finally
                    //{
                    //    Monitor.Exit(obj);
                    //}
                }
            });

            t1.Start();

            Thread t2 = new Thread(() => {
                for (int i = 0; i < 100000000; i++)
                {
                    lock (obj)
                        num++;
                    //try
                    //{
                    //    Monitor.Enter(obj);
                    //    num++;
                    //}
                    //finally
                    //{
                    //    Monitor.Exit(obj);
                    //}
                }
            });

            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine(num);
        }
    }
}