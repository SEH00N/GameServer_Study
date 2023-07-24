using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketTest2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server();
            //GenerateTCPListener(); 
            //GenerateTCPClient();
        }
        
        static void Server()
        {
            // 서버 생성
            TcpListener tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8081);
            
            // 서버 오픈
            tcpListener.Start();
            Console.WriteLine("대기상태");

            // 클라이언트 접속 대기
            TcpClient tcpClient = tcpListener.AcceptTcpClient();
            // 클라이언트 소켓의 스트림 생성
            NetworkStream ns = tcpClient.GetStream();

            // 클라이언트에서 보낸 메세지 읽기
            byte[] receiveMessage = new byte[100];
            ns.Read(receiveMessage, 0, 100);

            // 바이트로 들어온 메세지 파싱 후 출력
            string strMessage = Encoding.ASCII.GetString(receiveMessage);
            Console.WriteLine(strMessage);

            // 보낼 메세지 바이트로 파싱 후 보내기
            string echoMessage = "Welcome to World!";
            byte[] sendMessage = Encoding.ASCII.GetBytes(echoMessage);
            ns.Write(sendMessage, 0, sendMessage.Length);

            // 모든 작업 후 스트림 닫기
            ns.Close();

            // 클라이언트 연결 끊기
            tcpClient.Close();

            // 서버 닫기
            tcpListener.Stop();

            Console.ReadKey();
        }

        #region 연결클래스
        //static void GenerateTCPListener()
        //{
        //    IPAddress ip = IPAddress.Parse("127.0.0.1");
        //    TcpListener tcpListener = new TcpListener(ip, 13);
        //    Console.WriteLine($"{tcpListener.LocalEndpoint.ToString()}");
        //    Console.ReadKey();
        //}

        //static void GenerateTCPClient()
        //{
        //    // 얘가 서버
        //    TcpListener tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);

        //    // TCP Listener 를 시작해서 입력 대기 상태로 전환
        //    tcpListener.Start();
        //    Console.WriteLine("대기 상태 시작");

        //    // 리스너를 오픈해서 클라이언트를 받아드릴 준비
        //    TcpClient tcpClient = tcpListener.AcceptTcpClient();
        //    Console.WriteLine("대기 상태 종료");

        //    tcpListener.Stop();
        //}
        #endregion
    }
}