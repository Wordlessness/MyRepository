using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawARope : MonoBehaviour
{
    public GameObject nail;
    public GameObject circle;
    public float radius = 0f;
    public float debug;
    private Vector3 CPos;
    private Vector3 NPos;
    Rigidbody2D tc;
    Rigidbody2D tn;
    LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        tc = circle.GetComponent<Rigidbody2D>();
        tn = nail.GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();
        DataManager.sendR.AddListener(OnEventTriggered);
    }
    void OnEventTriggered(float r)
    {
        radius = r;
    }
    // Update is called once per frame
    void Update()
    {
        CPos = tc.position;
        NPos = tn.position;
        debug = (CPos - NPos).magnitude;
        if (Mathf.Abs((CPos - NPos).magnitude) >= (radius == 0 ? 100f : radius-0.04f)) {
            line.startColor = Color.red;
            line.endColor = Color.red;
            DrawLine(CPos, NPos);
        } 
        else {
            line.startColor = Color.white;
            line.endColor = Color.white;
            DrawLine(CPos, NPos);
        } 
    }
    void DrawLine(Vector2 CPos, Vector2 NPos)
    {
        line.positionCount = 2;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.SetPosition(0, CPos);
        line.SetPosition(1, NPos);
    }
}
