using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class NetworkHost : MonoBehaviour
{
    private static NetworkHost instance = null;
    public static NetworkHost Instance => instance;

    [SerializeField] int port = 8081;
    private Socket listenerSocket;
    private Socket clientSocket;

    private NetworkRouter router = null;

    private void Awake()
    {
        instance = this;

        try {
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            listenerSocket.Listen(1);

            router = new NetworkRouter(clientSocket);
            router.CreateDispatchThread(DispatchLoop);
        } catch {
            Debug.Log("서버 연결 실패");
        }

        List<Handler> handlers = new List<Handler>();
        transform.GetComponentsInChildren<Handler>(handlers);
        foreach(Handler handler in handlers)
            router.RegisterHandler(handler.type, handler.Handle);
    }

    private void Update()
    {
        router.TaskProcess();
    }

    private void DispatchLoop()
    {
        if (clientSocket != null)
        {
            if (clientSocket.Connected != false)
            {
                router.DispatchRequest();
                router.DispatchResponse();
            }
        }
        else
            AcceptClient();
    }

    private void AcceptClient()
    {
        if(listenerSocket != null && listenerSocket.Poll(0, SelectMode.SelectRead))
        {
            clientSocket = listenerSocket.Accept();
            router.SetSocket(clientSocket);
            if(clientSocket.Connected)
                Debug.Log("Connected");
        }
    }

    public void SendPacket(Packet packet) => router.SendPacket(packet);
}
