using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
public class testObj : Objects
{
    public testObj()
    {
        objName = "OBJET DE TEST";
        value = 42;
        ico.GetComponent<RawImage>().texture = Resources.Load<Texture>("T18");
    }

    override public void ApplyEffect()
    {
        foreach(GameObject p in Board.Instance.pieces)
        {
            p.GetComponent<PlayerPiece>().defMouvPoints++;
        }
    }
}
