using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;

[System.Serializable]
public class MessageEvent : UnityEvent<float> { }
public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public float Ex;
    public float Ey;
    public float Q;
    public float V0;
    public float M;
    public float R;
    public static MessageEvent sendR = new();
    int f = 1;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
            
        else
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (f == 1 && R != 0)
        {
            sendR.Invoke(R);
            f = 0;
        }
    }
}
