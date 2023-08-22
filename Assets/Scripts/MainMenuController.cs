using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    // Will controls actions of main menu from here
    public void PlayGame()
    {
        // This function will be called when user clicks either of the character selection button-img
        // We will get the name of character from the event and see which one was clicked and then convert to int and use it to spawn respective player

        int playerSelected = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        
        // we will give this index of player selection to the GameController Script/Instance that will load the respective player and ensures that it has only one instance using singleton pattern
        
        // Lets set this int into the GameController and tell him what was selected and then spawn that in there
        GameController.Instance.CharacterIndex = playerSelected;
        
        /*Debug.Log($"Player Selected: {playerSelected}");*/
        
        SceneManager.LoadScene("GamePlay");
    }
}
