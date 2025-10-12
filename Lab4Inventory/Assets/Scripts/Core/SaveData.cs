using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Datos que se guardan/cargan del juego.
/// </summary>
[Serializable]
public class SaveData
{
    public int coins;              
    public int keys;               
    public float hp;               

    public float posX, posY, posZ;

    public bool doorOpened;

    public float speedBuffTimeLeft;

    public List<string> collectedPickupIds = new List<string>();
    public List<String> inventoryKinds = new();

    public void SetPos(Vector3 p)
    {
        posX = p.x;
        posY = p.y;
        posZ = p.z;
    }

    public Vector3 GetPos()
    {
        return new Vector3(posX, posY, posZ);
    }
}
