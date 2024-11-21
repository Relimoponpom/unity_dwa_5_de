using System;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;


[Serializable]
public class SaveData
{
    public static SaveData instance;
    public int collectiblesCount;
    public float playerX, playerY, playerZ;
    public SaveData()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        instance = this;
    }

    public SaveData(SaveData newData)
    {
        instance = newData;

    }
}