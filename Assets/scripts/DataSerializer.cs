using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataSerializer
{
    private static string saveFilePath = "C:\\saves\\game.save";  // Save file path

    public static void Save()
    {
        if (SaveData.Instance == null)
        {
            Debug.LogError("Cannot save, no game data available.");
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists("C:\\saves"))
        {
            Directory.CreateDirectory("C:\\saves");
        }

        FileStream file = File.Create(saveFilePath);
        formatter.Serialize(file, SaveData.Instance);
        file.Close();

        Debug.Log("Game saved successfully.");
    }

    public static bool SaveFileExists()
    {
        return File.Exists(saveFilePath);  // Check if the save file exists
    }

    public static void Load()
    {
        if (!SaveFileExists())
        {
            Debug.LogError("Save file not found.");
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(saveFilePath, FileMode.Open);

        try
        {
            SaveData.Instance = (SaveData)formatter.Deserialize(file);  // Load save data into singleton instance
            Debug.Log("Game loaded successfully.");
        }
        catch
        {
            Debug.LogError("Failed to load save data.");
        }
        finally
        {
            file.Close();
        }
    }
}