using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataSerializer : MonoBehaviour
{

    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //Application.persistentDataPath + "\\saves";

        if (!Directory.Exists("D:\\saves"))
        {
            Directory.CreateDirectory("D:\\saves");
        }

        FileStream file = File.Create("D:\\saves\\game.save");
        formatter.Serialize(file, SaveData.instance);

        file.Close();
    }

    public static bool AnySaves()
    {
        if (!Directory.Exists("D:\\saves"))
        {
            return false;
        }
        if (!File.Exists("D:\\saves\\game.save"))
        {
            return false;

        }

        return true;
    }

    public static void Load()
    {
        if (!Directory.Exists("D:\\saves"))
        {
            Debug.LogError("Directory was not found");
            return;
        }
        if (!File.Exists("D:\\saves\\game.save"))
        {
            Debug.LogError("File was not found");
            return;

        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open("D:\\saves\\game.save", FileMode.Open);

        try
        {
            SaveData data = new SaveData((SaveData)formatter.Deserialize(file));
            file.Close();
            
        }
        catch
        {
            Debug.LogError("Cannot get savedata");

        }

    }
}

