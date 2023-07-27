using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class NetworkClient : MonoBehaviour
{
    private static NetworkClient instance = null;
    public static NetworkClient Instance => instance;

    [SerializeField] int port = 8081;

    private NetworkRouter router = null;
    private Socket serverSocket = null;

    private void Awake()
    {
        instance = this;

        try {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Connect(GameManager.Instance.TargetIP, port);

            if(serverSocket.Connected)
            {
                router = new NetworkRouter(serverSocket);
                router.CreateDispatchThread(DispatchLoop);
                Debug.Log("접속 성공");
                SendPacket(new Packet(EventType.Connected, "ㅋㅋ"));
            }
            else
            {
                Debug.Log("서버 접속 실패");
                serverSocket = null;
            }
        } catch {}

        List<Handler> handlers = new List<Handler>();
        transform.GetComponentsInChildren<Handler>(handlers);
        foreach (Handler handler in handlers)
            router.RegisterHandler(handler.type, handler.Handle);
    }

    private void Update()
    {
        router.TaskProcess();
    }

    private void DispatchLoop()
    {
        if (serverSocket != null)
        {
            if (serverSocket.Connected != false)
            {
                router.DispatchRequest();
                router.DispatchResponse();
            }
        }
    }

    public void SendPacket(Packet packet) => router.SendPacket(packet);
}
