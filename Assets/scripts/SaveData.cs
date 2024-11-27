using System;

[Serializable]
public class SaveData
{
    public static SaveData Instance; // Singleton Instance
    public int collectiblesCount;
    public float playerX, playerY, playerZ;

    // Constructor for a new save
    public SaveData()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        Instance = this;
    }

    // Constructor to load existing data
    public SaveData(SaveData newData)
    {
        Instance = newData;
    }
}
