using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance {
        get {
            if(instance == null)
                instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }

    public string TargetIP = "";

    private Text logText = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void SetLogText(string log)
    {
        if(logText == null)
            logText = GameObject.Find("Canvas/LogBox/LogText").GetComponent<Text>();

        logText.text = logText.text + '\n' + log;
        // Debug.Log(logText.text);
    }
}
