using System.Collections.Generic;
using UnityEngine;

public class TDict : MonoBehaviour
{
    private void Awake()
    {
        Dictionary<string, string> d = new Dictionary<string, string>();
        string a = d["asd"];
        Debug.Log(a);
    }
}
