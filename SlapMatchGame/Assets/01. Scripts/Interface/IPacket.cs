using System.Text;
using System;

public interface IPacket
{
    public EventType Type {get;}
    public string Serialize();

    public byte[] ToBuffer()
    {
        string strData = Serialize();
        byte[] byteData = Encoding.ASCII.GetBytes(strData);
        
        byte[] value = new byte[byteData.Length + 2];
        Array.Copy(byteData, 0, value, 2, byteData.Length);
        value[0] = (byte)byteData.Length;
        value[1] = (byte)Type;

        return value;
    }
}
