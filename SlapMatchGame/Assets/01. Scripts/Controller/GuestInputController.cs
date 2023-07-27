using UnityEngine;

public class GuestInputController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    private Packet movePacket = new Packet(EventType.MoveReq);
    private float lastUpdateTime = 0f;

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float delta = vertical * speed * Time.deltaTime;
        movePacket.strData = (transform.position.y + delta).ToString();

        if(Time.time - lastUpdateTime > DEFINE.TickBound / 1000f)
        {
            if(transform.position.y.ToString() != movePacket.strData)
            {
                lastUpdateTime = Time.time;
                NetworkClient.Instance.SendPacket(movePacket);
            }
            //Debug.Log("움직임 요청");
        }
    }
}
