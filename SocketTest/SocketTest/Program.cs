using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.WebSockets;

namespace SocketTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IPParsing();
            //DNS();
            //DNStoHostEntry();
            //IPEndPoint();
        }

        #region 정보 클래스
        static void IPParsing()
        {
            // string 형태의 IP를 long 형태로 파싱하는 방법
            string address = Console.ReadLine();
            IPAddress ip = IPAddress.Parse(address);
            Console.WriteLine($"ip : {ip.ToString()}");
        }

        static void DNS()
        {
            // 네이버가 갖고있는 IP가 출력됨
            // 여러 곳에 접속 주소를 분산배치해 트래픽 분리
            // 따라서 도메인의 IP를 얻을 땐 배열로 받을 수 있음
            IPAddress[] ip = Dns.GetHostAddresses("www.naver.com");
            foreach(IPAddress hostIP in ip)
                Console.WriteLine($"{hostIP}");
        }

        static void DNStoHostEntry()
        {
            IPHostEntry hostInfo = Dns.GetHostEntry("www.naver.com");

            foreach (IPAddress ip in hostInfo.AddressList)
                Console.WriteLine($"{ip}");
            Console.WriteLine($"{hostInfo.HostName}");
        }

        static void IPEndPoint()
        {
            IPAddress ipInfo = IPAddress.Parse("127.0.0.1");
            int port = 13;
            IPEndPoint endPointInfo = new IPEndPoint(ipInfo, port); //나의 아이피의 13번 포트로 EndPoint 생성
            
            Console.WriteLine($"ip : {endPointInfo.Address}, port : {endPointInfo.Port}");
            Console.WriteLine(endPointInfo.ToString());
            Console.ReadKey();
        }
        #endregion
    }
}