using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderListener : MonoBehaviour
{
    public Slider SEx;
    public Slider SEy;
    public Slider SQ;
    public Slider SV0;
    public Slider SM;
    // Start is called before the first frame update
    void Start()
    {
        SEx.value = DataManager.instance.Ex;
        SEy.value = DataManager.instance.Ey;
        SQ.value = DataManager.instance.Q;
        SV0.value = DataManager.instance.V0;
        SM.value = DataManager.instance.M;
        SEx.onValueChanged.AddListener(newValue => Value(newValue, "Ex"));
        SEy.onValueChanged.AddListener(newValue => Value(newValue, "Ey"));
        SQ.onValueChanged.AddListener(newValue => Value(newValue, "Q"));
        SV0.onValueChanged.AddListener(newValue => Value(newValue, "V0"));
        SM.onValueChanged.AddListener(newValue => Value(newValue, "M"));
    }

    // Update is called once per frame
    public void Value(float value, string name)
    {
        switch (name)
        {
            case "Ex":
                DataManager.instance.Ex = value; 
                break;
            case "Ey":
                DataManager.instance.Ey = value;
                break;
            case "Q":
                DataManager.instance.Q = value;
                break;
            case "M":
                DataManager.instance.M = value;
                break;
            case "V0":
                DataManager.instance.V0 = value;
                break;
        }
    }
    void Update()
    {
        
    }
}
