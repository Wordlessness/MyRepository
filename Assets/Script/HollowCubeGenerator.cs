using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MyHollowCube
{
    public GameObject Cube;
    public MyHollowCube next;
    public float length;
    public Vector3 deltaRotate;
    public Vector3 velocity;
    public double k;
    public double b;
    public float vanishPosition;
    public Material[] material;
    public MyHollowCube()
    {
        material = new Material[12];
    }
}
public class HollowCubeGenerator : MonoBehaviour
{
    public GameObject prefab;
    private int rd;
    private int zposition;
    private MyHollowCube head = null;
    private MyHollowCube tail = null;
    private float l = 18;
    private float w = 9;
    private void Awake()
    {
        rd = 0;
        zposition = 120;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rd == 0)
        {
            rd = UnityEngine.Random.Range(20, 50);
            MyHollowCube myCube = CubeGeneration();
            if (head == null) {
                head = myCube;
                tail = myCube;// list is empty
            }
            else{
                tail.next = myCube;
                tail = myCube;
            }
        }
        else rd--;
        MyHollowCube p = head;
        MyHollowCube pre = null;
        MyHollowCube temp;
        while (p != null)
        {
            Transform transform = p.Cube.transform;
            Renderer cubeRenderer = p.Cube.GetComponentInChildren<Renderer>();
            float deltaTime = Time.deltaTime;
            transform.position += deltaTime * p.velocity;
            transform.Rotate(deltaTime * p.deltaRotate);
            double color = p.k * transform.position.y + p.b;
            for (int i = 0; i < 12; i++)
            {
                p.material[i].SetColor("_EmissionColor", new Vector4((float)color, (float)color, (float)color, 1f));
            }
            if (transform.position.y > p.vanishPosition)
            {
                if (head == p)
                {
                    head = p.next;
                    if (tail == p) tail = null;
                }
                else if (tail == p)
                {
                    tail = pre;
                    pre.next = p.next;
                }
                else pre.next = p.next;
                temp = p;
                p = p.next;
                Destroy(temp.Cube);
                temp = null;
            }
            else {
                pre = p;
                p = p.next;
            }
        }
    }
    MyHollowCube CubeGeneration()
    {
        MyHollowCube a = new MyHollowCube();
        GameObject myCube = Instantiate(prefab);
        a.Cube = myCube;
        a.next = null;
        a.length = UnityEngine.Random.Range(1f, 11f);
        float para = (a.length - 1) / 4 + 2;//for constrict velocity
        a.deltaRotate = new Vector3(UnityEngine.Random.Range(-90, 90) / para, UnityEngine.Random.Range(-90, 90) / para, UnityEngine.Random.Range(-90, 90) / para);
        a.velocity = new Vector3(0, UnityEngine.Random.Range(10,20) / para, 0);
        
        a.vanishPosition = UnityEngine.Random.Range(-3f, 3f)- a.length;
        a.k = (1f - (58f / 255f)) / (-w - a.length - 1 - a.vanishPosition);
        
        a.b = 1 + (w + a.length + 1) * a.k;
        Transform transform = myCube.transform;
        transform.localScale = new Vector3(a.length, a.length, a.length);
        transform.position = new Vector3(UnityEngine.Random.Range(-l, l), -w - a.length - 1, zposition);
        if (zposition >=390) zposition = 120;
        else zposition += 15;
        int i = 0;
        foreach (Transform child in transform)
        {
            Renderer childRenderer = child.GetComponent<Renderer>();
            Material childMaterial = childRenderer.material;
            a.material[i] = childMaterial;
            i++;
        }
        return a;
    }
}
