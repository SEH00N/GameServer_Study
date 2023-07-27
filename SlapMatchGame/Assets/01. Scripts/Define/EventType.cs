using UnityEngine;

public enum EventType
{
    None,

    // Client Side
    Connected,
    MoveRes,
    OtherMove,
    
    // Server Side
    MoveReq
}
