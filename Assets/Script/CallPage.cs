using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CallPage : MonoBehaviour, IPointerUpHandler
{
    public GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        image.SetActive(false);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        image.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
