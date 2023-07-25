using System.Net.Sockets;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[] buffer = new char[128];

            TcpClient serverSocket = new TcpClient("127.0.0.1", 8081);
            NetworkStream ns = serverSocket.GetStream();

            using (StreamReader reader = new StreamReader(ns))
            {
                string? str = reader.ReadLine();
                Console.WriteLine(str);

                str = reader.ReadLine();
                Console.WriteLine(str);

                str = reader.ReadLine();
                Console.WriteLine(str);

                str = reader.ReadLine();
                Console.WriteLine(str);
            }

            ns.Close();
            serverSocket.Close();
        }
    }
}