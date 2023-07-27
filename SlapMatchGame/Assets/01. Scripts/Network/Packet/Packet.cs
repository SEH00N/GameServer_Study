using System.Net.Sockets;
using UnityEngine;

public class Packet : IPacket
{
    public EventType type;
    public string strData;

    public Packet(EventType type = EventType.None, string strData = "")
    {
        this.type = type;
        this.strData = strData;
    }

    public EventType Type => type;
    public string Serialize() => strData;
}
