using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour,IPointerUpHandler
{
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        SceneManager.LoadScene(sceneName);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
