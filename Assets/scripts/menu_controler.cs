using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel; 

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            menuPanel.SetActive(!menuPanel.activeSelf); // Toggle the active state of the menuPanel
        }
    }

    public void NewGame()
    {
        new SaveData();
    }

    public void Continue()
    {
        // Add functionality for continuing the game here
    }

    public void Save()
    {
        // Fill save data
        DataSerializer.Save();
    }

    public void Exit()
    {
        // Add functionality for exiting the game here
    }
}
