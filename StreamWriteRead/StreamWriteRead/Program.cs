using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;

namespace StreamWriteRead
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8081);
            listener.Start();

            TcpClient clientSocket = listener.AcceptTcpClient();

            bool flag = false;
            int value = 12;
            float pi = 3.14f;
            string message = "Hello, World!";

            NetworkStream ns = clientSocket.GetStream();
            using(StreamWriter writer = new StreamWriter(ns))
            {
                writer.AutoFlush = true; // 자동 버퍼 클리어
                writer.WriteLine(flag);
                writer.WriteLine(value);
                writer.WriteLine(pi);
                writer.WriteLine(message);
            }

            ns.Close();
            clientSocket.Close();
            listener.Stop();
        }
    }
}