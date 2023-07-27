using UnityEngine;

public class GuestConnectHandler : Handler
{
    [SerializeField] GameObject blockPanel = null;

    public override void Handle(Packet packet)
    {
        blockPanel.SetActive(false);
        GameManager.Instance.SetLogText("상대방 접속!");
    }
}
