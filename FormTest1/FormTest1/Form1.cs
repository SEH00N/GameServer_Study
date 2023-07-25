using System.Drawing.Design;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

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

        private void Form1_Load(object sender, EventArgs e)
        {
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8081);
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
            clientSocket = listener.AcceptTcpClient();
            if(clientSocket.Connected)
            {
                txt2.Text = ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString();
            }

            ns = clientSocket.GetStream();
            bw = new BinaryWriter(ns);
            br = new BinaryReader(ns);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            while(true)
            {
                if(clientSocket.Connected)
                {
                    if(DataReceive() == -1)
                    {
                        break;
                    }

                    DataSend();
                }
                else
                {
                    AllClose();
                    break;
                }
            }

            AllClose();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AllClose();
            listener.Stop();
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