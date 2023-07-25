using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[Client Side]");

            byte[] buffer = new byte[1024];
            int readedByteCount = 0;
            int totalByteCount = 0;

            // 종단점이 127.0.0.1:8081인 소켓 생성 (서버)
            TcpClient serverSocket = new TcpClient("127.0.0.1", 8081);
            // 서버와 송수신 할 스트림 생성
            NetworkStream ns = serverSocket.GetStream();

            // Hello, World!
            byte[] sendMessage = StringToBuffer("Hello, World!");

            // 메세지 전송
            SendMessage(ns, sendMessage);

            while (true)
            {
                // 메세지 수신 & 출력
                readedByteCount = ReadMessage(ns, buffer);
                Console.WriteLine($"RBC : {readedByteCount}");

                totalByteCount += readedByteCount;
                Console.WriteLine($"TBC : {totalByteCount}");

                if (totalByteCount > 900)
                    break;

                // 수신한 메세지 전송
                SendMessage(ns, buffer, readedByteCount);
            }

            Console.WriteLine($"TBC : {totalByteCount}");
            ns.Close();
            serverSocket.Close();

            // 받은 메세지 에코 전송
            //SendMessage(ns, buffer);
        }

        static byte[] StringToBuffer(string str) => Encoding.ASCII.GetBytes(str);
        static void PrintBuffer(byte[] buffer) => Console.WriteLine(Encoding.ASCII.GetString(buffer));

        static int ReadMessage(NetworkStream ns, byte[] buffer)
        {
            int result = ns.Read(buffer, 0, buffer.Length);
            Console.Write("[Read Message] ");
            PrintBuffer(buffer);

            return result;
        }
        static void SendMessage(NetworkStream ns, byte[] buffer, int length = -1)
        {
            if (length == -1)
                length = buffer.Length;

            Console.Write("[Send Message] ");
            PrintBuffer(buffer);
            ns.Write(buffer, 0, length);
        }
    }
}