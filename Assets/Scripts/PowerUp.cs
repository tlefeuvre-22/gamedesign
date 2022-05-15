using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PowerUpType
{
    shield
}
public static class PowerUp
{
    public static PowerUpType PUtype;
    public static void Effect(Piece p)
    {
        if (PUtype == PowerUpType.shield)
        { 
            p.hasShield = true;
            p.shieldGO.SetActive(true);
        }
    }
}
