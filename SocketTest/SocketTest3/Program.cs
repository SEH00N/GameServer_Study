using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;

namespace SocketTest3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Client();
            //GenerateTCPClient();
        }

        static void Client()
        {
            Console.WriteLine("[클라이언트]");

            // 127.0.0.1:8081 주소의 서버로 연결할 소켓 생성
            TcpClient tcpClient = new TcpClient("127.0.0.1", 8081);

            // 소켓이 연결 됐으면
            if (tcpClient.Connected)
            {
                Console.WriteLine("서버 연결 성공");

                // 서버 소켓의 스트림 생성
                NetworkStream ns = tcpClient.GetStream();

                // 메세지 파싱해서 전송
                string message = "Hello, World!";
                byte[] sendMessage = Encoding.ASCII.GetBytes(message);
                ns.Write(sendMessage, 0, sendMessage.Length);

                // 메세지 읽기
                byte[] receiveByteMessage = new byte[32];
                ns.Read(receiveByteMessage, 0, 32);

                // 읽은 메세지 파싱해서 출력
                string receiveMessage = Encoding.ASCII.GetString(receiveByteMessage);
                Console.WriteLine(receiveMessage);

                // 스트림 종료
                ns.Close();
            }
            else
            {
                Console.WriteLine("서버 연결 실패!");
            }

            tcpClient.Close();
            Console.ReadKey();
        }

        //static void GenerateTCPClient()
        //{
        //    // 127.0.0.1:8080로 연결할 클라이언트 생성
        //    TcpClient tcpClient = new TcpClient("127.0.0.1", 8080);
            
        //    if(tcpClient.Connected)
        //        Console.WriteLine("서버 연결 성공");
        //    else
        //        Console.WriteLine("서버 연결 실패");

        //    tcpClient.Close();
        //    Console.ReadKey();
        //}
    }
}