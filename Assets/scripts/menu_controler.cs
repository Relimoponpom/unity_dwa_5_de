using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // For UI interaction

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel;
    public Button continueButton;  // Reference to the "Continue" button

    private bool gameStarted = false; // Track if the game has started (to control menu behavior)

    // Called when the object is created (called only once, on object creation)
    public void Start()
    {
        // Check if the save file exists when the game starts
        CheckSaveFileExistence();  // This sets the interactability of the Continue button
    }

    // Called every frame
    public void Update()
    {
        // Press Escape to toggle the menu
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(gameStarted)
            {
                menuPanel.SetActive(!menuPanel.activeSelf);
            }
        }
    }

    // Called when starting a new game
    public void NewGame()
    {
        // Initialize a new game and SaveData instance
        SaveData.Instance = new SaveData();
        DataSerializer.Save();  // Save the initial game state
        continueButton.interactable = true;  // Enable Continue button after new game starts
        menuPanel.SetActive(false);  // Hide the menu immediately after new game
        gameStarted = true;  // Mark the game as started
        Debug.Log("New game started.");
    }

    // Called when continuing from a save file
    public void Continue()
    {
        // Ensure we load the saved data and hide the menu
        DataSerializer.Load();
        menuPanel.SetActive(false);
        gameStarted = true;  // Mark the game as started
    }

    // Called to save the current game
    public void Save()
    { 
        Debug.Log("test");
        if (SaveData.Instance != null)
        {
            DataSerializer.Save();  // Call the Save method from DataSerializer to save the data
            continueButton.interactable = true;  // Enable the Continue button after saving
            Debug.Log("Game saved successfully.");
        }
        else
        {
            Debug.LogError("Cannot save, no game data available.");
        }
    }

    // Called to exit the game
    public void Exit()
    {
        // Handle game exit logic here, like quitting the game or loading the main menu
        Debug.Log("Exiting game.");
        Application.Quit();
    }

    // Called when the menu is displayed to check if save file exists
    public void CheckSaveFileExistence()
    {
        continueButton.interactable = DataSerializer.SaveFileExists();  // Enable/disable based on file existence
    }
}