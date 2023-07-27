using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    [SerializeField] TMP_InputField ipText;

    public void HostServer()
    {
        GameManager.Instance.TargetIP = ipText.text;
        SceneManager.LoadScene("HostScene");
    }

    public void JoinServer()
    {
        GameManager.Instance.TargetIP = ipText.text;
        SceneManager.LoadScene("GuestScene");
    }
}
