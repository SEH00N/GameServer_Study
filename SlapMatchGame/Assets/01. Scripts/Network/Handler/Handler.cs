using UnityEngine;

public abstract class Handler : MonoBehaviour
{
    public EventType type;
    public abstract void Handle(Packet packet);
}
