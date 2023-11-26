using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeUITextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject parentObject;
    public TMP_Text text = null;
    public Transform child;
    public Color color_enter, color_click;
    // Start is called before the first frame update
    void Start()
    {
        child = transform.Find("Text (TMP)");
        text = child.GetComponent<TMP_Text>();

        color_enter = new Color(210f / 255f, 210f / 255f, 210f / 255f, 255f / 255f);
        color_click = new Color(145f / 255f, 145f / 255f, 145f / 255f, 255f / 255f);
        //Todo: create more color setting.
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = color_enter;

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.white;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        text.color = color_click;
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        text.color = color_enter;
        if (parentObject == null) Debug.Log("you forget parent object!");
        if (parentObject != null)
        {
            ExecuteEvents.Execute<IPointerUpHandler>(parentObject, eventData, (x, y) => x.OnPointerUp((PointerEventData)y));
        }

    }
    private void OnEnable()
    {
        if(text!=null) text.color = Color.white;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
