using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class NetworkRouter
{
    private int tickBound = 1000 / 20;
    private Socket socket;

    private Thread dispatchThread;
    private bool threadLoop = false;

    private Dictionary<EventType, Action<Packet>> handlers = new Dictionary<EventType, Action<Packet>>();
    private Queue<Packet> taskQueue = new Queue<Packet>();
    private Queue<Packet> packetQueue = new Queue<Packet>();

    public NetworkRouter(Socket socket, int tickBound = 1000 / 20)
    {
        this.socket = socket;
        this.tickBound = tickBound;
    }

    public void SetSocket(Socket socket)
    {
        this.socket = socket;
    }

    public void CreateDispatchThread(Action dispatchLoop)
    {
        dispatchThread = new Thread(() => {
            while(threadLoop) {
                dispatchLoop?.Invoke();
                Thread.Sleep(tickBound);
            }
        });
        threadLoop = true;
        dispatchThread.Start();
    }

    public void TaskProcess()
    {
        while (taskQueue.Count > 0)
        {
            Packet packet = taskQueue.Dequeue();
            UnityEngine.Debug.Log($"{packet.type} | {packet.strData}");

            if (handlers.ContainsKey(packet.type))
                handlers[packet.type]?.Invoke(packet);
        }
    }

    public void DispatchRequest()
    {
        try {
            int offset = 0;
            List<byte> buffer = new List<byte>();

            while(socket.Poll(0, SelectMode.SelectRead)) {
                byte[] register = new byte[1024];
                int registerLength = socket.Receive(register, register.Length, SocketFlags.None);
                
                if(registerLength <= 0)
                    break;

                buffer.AddRange(register);
            }
            buffer.Add(0);

            while(true)
            {
                int length = buffer[offset];
                if(length == 0)
                {
                    offset = -1;
                    break;
                }

                byte[] register = buffer.GetRange(offset + 1, length).ToArray();
                string strData = Encoding.ASCII.GetString(register);
                Packet packet = new Packet((EventType)register[0], strData);

                taskQueue.Enqueue(packet);
                offset += length + 2;
            }
        }catch {
        }
    }

    public void DispatchResponse()
    {
        object packetQueueLocker = new object();

        try {
            if(socket.Poll(0, SelectMode.SelectWrite)) {
                List<byte> buffer = new List<byte>();
                lock(packetQueueLocker) {
                    while(packetQueue.Count > 0) {
                        IPacket packet = packetQueue.Dequeue();
                        buffer.AddRange(packet.ToBuffer());
                    }
                }

                socket.Send(buffer.ToArray());
            }
        } catch {}
    }

    public void RegisterHandler(EventType type, Action<Packet> handler)
    {
        if(handlers.ContainsKey(type) == false)
            handlers.Add(type, null);

        handlers[type] += handler;
    }

    public void UnregisterHandler(EventType type, Action<Packet> handler)
    {
        if(handlers.ContainsKey(type) == false)
            return;

        handlers[type] -= handler;
    }

    public void SendPacket(Packet packet)
    {
        packetQueue.Enqueue(packet);
    }
}
