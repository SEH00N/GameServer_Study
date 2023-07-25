using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;

namespace Socket03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[Server Side]");

            // 서버 생성
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8081);

            // 서버 오픈
            listener.Start();
            Console.WriteLine($"[Server] server is running on {listener.LocalEndpoint}");

            // 데이터를 저장하고 보낼 버퍼 생성
            byte[] buffer = new byte[1024];
            int readedByteCount = 0;
            int totalByteCount = 0;

            // 클라이언트와 연결 & 송수신 할 스트림 생성
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream ns = client.GetStream();

            while(true)
            {
                // 스트림으로 메세지 수신 & 출력
                readedByteCount = ReadMessage(ns, buffer);
                Console.WriteLine($"RBC : {readedByteCount}");

                if (readedByteCount == 0)
                    break;

                // 수신한 메세지 전송
                SendMessage(ns, buffer, readedByteCount);

                totalByteCount += readedByteCount;
                Console.WriteLine($"TBC : {totalByteCount}");
            }

            // 스트림 종료
            ns.Close();
            // 클라이언트 연결 종료
            client.Close();
            // 서버 종료
            listener.Stop();
        }

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