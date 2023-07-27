using UnityEngine;

public class MoveRequestHandler : Handler
{
    [SerializeField] Vector2 limit;
    [SerializeField] Transform board;

    public override void Handle(Packet packet)
    {
        Debug.Log(packet.strData);
        float delta = JsonUtility.FromJson<float>(packet.strData);
        delta = Mathf.Clamp(delta, limit.x, limit.y);

        Packet sendPacket = new Packet(EventType.MoveRes, delta.ToString());
        NetworkHost.Instance.SendPacket(sendPacket);
        
        Vector3 pos = board.position;
        pos.y = delta;
        board.position = pos;
    }
}
