using UnityEngine;

public class MoveResponseHandler : Handler
{
    [SerializeField] Transform board = null;

    public override void Handle(Packet packet)
    {
        float delta = float.Parse(packet.strData);
        board?.Translate(0, delta, 0);
    }
}
