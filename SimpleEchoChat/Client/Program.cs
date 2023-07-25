using System.Net.Sockets;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpClient serverSocket = new TcpClient("127.0.0.1", 8081);
            NetworkStream ns = serverSocket.GetStream();

            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);

            while (true)
            {
                writer.AutoFlush = true;

                Console.Write("메세지 보내기 : ");
                string? str = Console.ReadLine();

                writer.WriteLine(str);

                if (str == "exit")
                    break;

                string? rec = reader.ReadLine();
                Console.WriteLine($"메세지 받음 : {rec}");
            }

            ns.Close();
            serverSocket.Close();
        }
    }
}