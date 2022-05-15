using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Objects : MonoBehaviour
{
    public string objName;
    public uint value;
    public GameObject ico;
    public Transform LayoutGroup;
    public Texture2D texture;

    protected Objects()
    {
        ico = Resources.Load("Prefabs/ObjectIco") as GameObject;
        LayoutGroup = Camera.main.transform.Find("Canvas").transform.Find("LayoutGroup");
    }
    virtual public void ApplyEffect()
    {
        //Implement the effect of the object
        throw new NotImplementedException();
    }
    virtual public void Display()
    {
        Instantiate(ico, LayoutGroup);
    }

}
