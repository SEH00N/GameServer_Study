using System.Text;
using System;
using UnityEngine;

public class TByte : MonoBehaviour
{
    public byte a;
    private void Awake()
    {
        // a = (byte)255;
        // // a = (byte)256;
        // Debug.Log(a);

        // ---

        // int[] arr = new int[] { 1, 2, 3 };
        // int[] arr2 = new int[4];

        // Array.Copy(arr, 0, arr2, 1, arr.Length);
        // arr2[0] = 10;

        // foreach(int i in arr2)
        //     Debug.Log(i);

        // Debug.Log(Convert.ToSByte(-1));

        // string str = "Hello, World!";
        // byte[] arr = BitConverter.GetBytes(1000);
        // foreach(byte b in arr)
        //     Debug.Log(b);

        // Debug.Log(BitConverter.ToInt32(arr));

        // Debug.Log((byte)EventType.Connected1);

        Debug.Log(float.Parse("-0.0174801"));
    }
}
