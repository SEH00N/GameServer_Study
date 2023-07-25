using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8081);
            listener.Start();

            TcpClient clientSocket = listener.AcceptTcpClient();
            NetworkStream ns = clientSocket.GetStream();

            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);

            while (true)
            {
                writer.AutoFlush = true;

                string? str = reader.ReadLine();
                Console.WriteLine($"메세지 받음 : {str}");

                if (str == "exit")
                    break;
                    
                writer.WriteLine(str);
            }

            ns.Close();
            clientSocket.Close();
            listener.Stop();
        }
    }
}