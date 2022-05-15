using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChestManager
{
    public static Camera cam;
    public static void AssingCamera(Camera cam)
    {
        ChestManager.cam = cam;
    }
    public static void ReciveObject()
    {
        Objects obj = new testObj();
        cam.GetComponent<Player>().inventory.Add(obj);//TODO chose the object
        obj.ApplyEffect();
        obj.Display();
        
    }
}
