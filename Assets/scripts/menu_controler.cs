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
            menuPanel.SetActive(!menuPanel.activeSelf);
        }
    }

    public void NewGame()
    {
        // Add functionality for starting a new game here
    }

    public void Continue()
    {
        // Add functionality for continuing the game here
    }

    public void Save()
    {
        // Add functionality for saving the game here
    } 

    public void Exit()
    {
        // Add functionality for exiting the game here
    }
}
