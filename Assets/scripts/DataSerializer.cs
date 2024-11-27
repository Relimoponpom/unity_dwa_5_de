using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataSerializer
{
    private static string saveFilePath = @"C:\saves\game.save";  // Correct path using verbatim string literal

    public static void Save()
    {
        if (SaveData.Instance == null)
        {
            Debug.LogError("Cannot save, no game data available.");
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        // Check if the directory exists, create it if not
        string directoryPath = Path.GetDirectoryName(saveFilePath);
        if (!Directory.Exists(directoryPath))
        {
            try
            {
                Directory.CreateDirectory(directoryPath);  // Create the directory if it doesn't exist
                Debug.Log("Directory created: " + directoryPath);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Failed to create directory: " + ex.Message);
                return;
            }
        }

        try
        {
            // Create and open the file stream
            FileStream file = File.Create(saveFilePath);
            formatter.Serialize(file, SaveData.Instance);  // Serialize the data into the file
            file.Close();
            Debug.Log("Game saved successfully to: " + saveFilePath);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to save the game: " + ex.Message);
        }
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
        try
        {
            FileStream file = File.Open(saveFilePath, FileMode.Open);
            SaveData.Instance = (SaveData)formatter.Deserialize(file);  // Deserialize the data into singleton instance
            file.Close();
            Debug.Log("Game loaded successfully from: " + saveFilePath);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to load save data: " + ex.Message);
        }
    }
}