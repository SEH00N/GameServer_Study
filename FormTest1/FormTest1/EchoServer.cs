using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WinFormServer
{
    public class EchoServer
    {
        private TcpClient clientSocket;
        private BinaryWriter bw = null;
        private BinaryReader br = null;

        private int intValue;
        private float floatValue;
        private string strValue;

        public EchoServer(TcpClient socket)
        {
            clientSocket = socket;
        }

        public void Process()
        {
            NetworkStream ns = clientSocket.GetStream();

            try
            {
                br = new BinaryReader(ns);
                bw = new BinaryWriter(ns);

                while(true)
                {
                    intValue = br.ReadInt32();
                    floatValue = br.ReadSingle();
                    strValue = br.ReadString();

                    bw.Write(intValue);
                    bw.Write(floatValue);
                    bw.Write(strValue);
                }
            }
            catch(SocketException err)
            {
                br.Close();
                bw.Close();
                ns.Close();
                ns = null;

                clientSocket.Close();
                MessageBox.Show(err.Message);
                Thread.CurrentThread.Interrupt();
            }
            catch(IOException err)
            {
                br.Close();
                bw.Close();
                ns.Close();
                ns = null;
                clientSocket.Close();
                Thread.CurrentThread.Interrupt();
            }
        }
    }
}
