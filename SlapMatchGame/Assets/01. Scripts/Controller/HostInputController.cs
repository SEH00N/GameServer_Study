using UnityEngine;

public class HostInputController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    private Packet movePacket = new Packet(EventType.OtherMove);
    private float lastUpdateTime = 0f;

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float delta = vertical * speed * Time.deltaTime;

        transform.Translate(0, delta, 0);

        movePacket.strData = (transform.position.y + delta).ToString();
        if(Time.time - lastUpdateTime > DEFINE.TickBound / 1000f)
        {
            lastUpdateTime = Time.time;
            NetworkHost.Instance.SendPacket(movePacket);
        }
    }
}
