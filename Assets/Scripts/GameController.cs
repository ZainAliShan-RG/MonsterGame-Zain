using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // This Script controls the Game, Which player will be spawn upon selection ?
    // Controls this flow
    
    // We will get arr of players from inspector and will apply logic to spawn respective player
    
    // 1. We need to make tell this script which player is selected by the user and then move that index(character selected info) in the gameplay scene
    // To move info from one scene to another we use Don't Destroy on Load
    // And to ensure that only one instance of GameController is made : we use singleton pattern
    // In singleton pattern only one instance of Game Controller will be created and we will set info about what character is selected to that instance from the main menu
    // To use vars across the scripts without needing to make it's object we use static vars

    public static GameController Instance;
    
    // Let's take arr of players/characters

    [SerializeField] private GameObject[] characters;

    // Let's make an index property to set the value in it that will be set from MainMenu
    
    public int CharacterIndex { get; set; }

    // let's create a single instance of the GameController in the Awake before even starting game

    private void Awake()
    {
        if (Instance == null) // If instance does not exist already
        {
            Instance = this; // make instance of this class and store it in our public static Instance
            DontDestroyOnLoad(gameObject); // and don't destroy the Instance if the scenes shift
        }
        else
        {
            // Means there was already an instance available : Destroy
            Destroy(gameObject);
        }
    }
    
    
    // Lets subscribe to an event
    // As soon as Scene is about to load, Instantiate the new player whose index we got
    // Let's Subscribe
    // Here SceneManager.Loaded is the Observer that subscribes to the method to spawn player
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SpawnPlayerWhenSceneLoads;
    }
    // Let's Also Unsubscribe when scene has loaded
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SpawnPlayerWhenSceneLoads;
    }

    private void SpawnPlayerWhenSceneLoads(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GamePlay")
        {
            GameObject playerInstance = Instantiate(characters[CharacterIndex]);
            // Check if there's a camera in the scene with the FollowCam script and assign the player to it directly
            FollowCam cameraScript = FindObjectOfType<FollowCam>();
            if(cameraScript != null)
            {
                Debug.Log($"Camera Script is not null ---  Here is the player: {playerInstance}");
                cameraScript.AssignPlayer(playerInstance.transform);
            }
        }
    }
}
