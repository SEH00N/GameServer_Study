using System.Net.Sockets;

namespace WinFormClient
{
    public partial class Form1 : Form
    {
        private TcpClient serverSocket = null;
        private NetworkStream ns = null;
        private BinaryReader br = null;
        private BinaryWriter bw = null;

        private int intValue;
        private float floatValue;
        private string strValue;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serverSocket = new TcpClient(textBox1.Text, 8081);
            if(serverSocket.Connected)
            {
                ns = serverSocket.GetStream();
                br = new BinaryReader(ns);
                bw = new BinaryWriter(ns);
                MessageBox.Show("서버 접속 성공");
            }
            else
                MessageBox.Show("서버 접속 실패");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bw.Write(int.Parse(textBox2.Text));
            bw.Write(float.Parse(textBox3.Text));
            bw.Write(textBox4.Text);

            intValue = br.ReadInt32();
            floatValue = br.ReadSingle();
            strValue = br.ReadString();

            string str = intValue + "/" + floatValue + "/" + strValue;
            MessageBox.Show(str);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bw.Write(-1);
            bw.Close();
            br.Close();
            ns.Close();
            serverSocket.Close();
        }
    }
}