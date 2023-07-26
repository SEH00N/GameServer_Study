
// TCP 실습 프로그램 - 유니티 연동 메인
#define USE_TRANSPORT_TCP

using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;


public class LibrarySample : MonoBehaviour {

	// 통신 모듈.
	public GameObject	transportTcpPrefab;
	public GameObject	transportUdpPrefab;

	// 통신용 변수.
#if USE_TRANSPORT_TCP
	TransportTCP		m_transport = null;
#else
	TransportUDP		m_transport = null;
#endif

	// 접속할 IP 주소.
	private string		m_strings = "";
	private string 		m_sendComment = "";
	private string 		m_receiveComment = "";

	// 접속할 포트 번호.
	private const int 	m_port = 50765;

	private const int 	m_mtu = 1400;

	private bool 		isSelected = false;


	// Use this for initialization
	void Start ()
	{
		// Transport 클래스의 컴포넌트를 가져온다.
#if USE_TRANSPORT_TCP
		GameObject obj = GameObject.Instantiate(transportTcpPrefab) as GameObject;
		m_transport = obj.GetComponent<TransportTCP>();
#else
		GameObject obj = GameObject.Instantiate(transportUdpPrefab) as GameObject;
		m_transport = obj.GetComponent<TransportUDP>();
#endif

        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        for (int i = 0; i < host.AddressList.Length; i++)   // IP저장된 배열 가져옴
        {
            if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
            {           // IP배열(Family)에서 내부IPv4 주소만 구분
                System.Net.IPAddress hostAddress = host.AddressList[i];
                Debug.Log(host.HostName);
                m_strings = hostAddress.ToString();
                break;  // for문 벗어남
            }

        }

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_transport != null && m_transport.IsConnected() == true) {
			byte[] buffer = new byte[m_mtu];
			int recvSize = m_transport.Receive(ref buffer, buffer.Length);
			if (recvSize > 0) {
				string message = System.Text.Encoding.UTF8.GetString(buffer);
				
				Debug.Log(message);
				m_receiveComment = message;
			}
		}
	}

	void OnGUI()
	{
		if (isSelected == false) {
			OnGUISelectHost();
		}
		else {
			if (m_transport.IsServer() == true) {
				OnGUIServer();
			}
			else {
				OnGUIClient();
			}
		}
	}

	void OnGUISelectHost()
	{
#if USE_TRANSPORT_TCP
		if (GUI.Button (new Rect (20,40, 150,20), "Launch server.")) {
#else
		if (GUI.Button (new Rect (20,40, 150,20), "Launch Listener.")) {
#endif
			m_transport.StartServer(m_port, 1);
			isSelected = true;
		}
		
		// 클라이언트를 선택했을 때 접속할 서버 주소를 입력합니다.
		m_strings = GUI.TextField(new Rect(20, 100, 200, 20), m_strings);
#if USE_TRANSPORT_TCP
			if (GUI.Button (new Rect (20,70,150,20), "Connect to server")) {
#else
			if (GUI.Button (new Rect (20,70,150,20), "Connect to terminal")) {
#endif
			m_transport.Connect(m_strings, m_port);
			isSelected = true;
			m_strings = "";
		}	
	}

	void OnGUIServer()
	{
#if USE_TRANSPORT_TCP
		if (GUI.Button (new Rect (20,100, 150,20), "Stop server")) {
#else
		if (GUI.Button (new Rect (20,100, 150,20), "Stop Listener")) {
#endif
			m_transport.StopServer();
			isSelected = false;
			m_strings = "";
		}

		GUI.TextField(new Rect(220, 400, 300, 30), $"받음 : {m_receiveComment}", 64);
        if (GUI.Button(new Rect(20, 70, 150, 20), "Send message")) {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(m_sendComment);
            m_transport.Send(buffer, buffer.Length);
        }
		else
			m_sendComment = GUI.TextField(new Rect(220, 440, 300, 30), m_sendComment, 64);
	}


	void OnGUIClient()
	{
		// 클라이언트를 선택했을 때 접속할 서버의 주소를 입력합니다.

		GUI.TextField(new Rect(220, 400, 300, 30), $"받음 : {m_receiveComment}", 64);

		if (GUI.Button (new Rect (20,70,150,20), "Send message")) {
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes(m_sendComment);	
			m_transport.Send(buffer, buffer.Length);
        }
		else
			m_sendComment = GUI.TextField(new Rect(220, 440, 300, 30), m_sendComment, 64);

		if (GUI.Button (new Rect (20,100, 150,20), "Disconnect")) {
			m_transport.Disconnect();
			isSelected = false;
			m_strings = "";
		}
	}

}
