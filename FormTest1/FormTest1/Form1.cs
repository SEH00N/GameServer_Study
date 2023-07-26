using System.Drawing.Design;
using System.Net;
using System.Net.Sockets;
using WinFormServer;

namespace FormTest1
{
    public partial class Form1 : Form
    {
        private TcpListener listener = null;
        private TcpClient clientSocket = null;
        private BinaryWriter bw = null;
        private BinaryReader br = null;
        private NetworkStream ns = null;

        private int intValue;
        private float floatValue;
        private string strValue;

        public Form1()
        {
            InitializeComponent();
        }

        private void AcceptClient()
        {
            while(true)
            {
                TcpClient clientSocket = listener.AcceptTcpClient();
                if(clientSocket.Connected)
                {
                    string str = ((IPEndPoint)clientSocket.Client.RemoteEndPoint).ToString();
                    listBox1.Items.Add(str);
                }

                EchoServer echoServer = new EchoServer(clientSocket);
                Thread th = new Thread(new ThreadStart(echoServer.Process));
                th.IsBackground = true;
                th.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listener = new TcpListener(IPAddress.Any, 8081);
            listener.Start();

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    txt1.Text = host.AddressList[i].ToString();
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(AcceptClient));
            th.IsBackground = true;
            th.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(listener != null)
            {
                listener.Stop();
                listener = null;
            }
        }

        private int DataReceive()
        {
            intValue = br.ReadInt32();
            if (intValue == -1)
                return -1;

            floatValue = br.ReadSingle();
            strValue = br.ReadString();

            string str = intValue + "/" + floatValue + "/" + strValue;
            MessageBox.Show(str);

            return 0;
        }

        private void DataSend()
        {
            bw.Write(intValue);
            bw.Write(floatValue);
            bw.Write(strValue);

            MessageBox.Show("º¸³Â½À´Ï´Ù");
        }

        private void AllClose()
        {
            if (bw != null)
            {
                bw.Close();
                bw = null;
            }
            if (br != null)
            {
                br.Close();
                br = null;
            }
            if (ns != null)
            {
                ns.Close();
                ns = null;
            }
            if (clientSocket != null)
            {
                clientSocket.Close();
                clientSocket = null;
            }
        }
    }
}