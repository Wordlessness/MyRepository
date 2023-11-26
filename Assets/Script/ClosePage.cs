using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClosePage : MonoBehaviour,IPointerUpHandler
{
    public GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (image == null) Debug.Log("you forget UI object!");
        image.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
