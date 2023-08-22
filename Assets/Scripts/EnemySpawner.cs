using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    // Let's get the array of enemies from inspector to spawn at the scene 
    // Will be spawned from left and right randomly
    // Will be spawned between 1-5 interval of seconds
    // A Random Index of array will be chosen to select and spawn the enemy

    // Get the array of game objects from inspector
    //public GameObject[] enemiesArr;

    // Get the left and right pos from inspector
    [SerializeField] private Transform leftSide;
    [SerializeField] private Transform rightSide;

    private GameObject _newEnemy; // To Instantiate new enemy

    private int _randomSide;
    private int _randomEnemyIndex;

    private float _spawnTime = 0.0f;
    private float _whenToSpawn = 0.0f;

    private Coroutine _spawnCoroutine;

    private readonly Dictionary<int, string> randomPlayer = new Dictionary<int, string>();
    
    // check if player is dead or not
    public static bool IsAlive = true;


    // Start is called before the first frame update
    private void Start()
    {
        // Add mappings to dict
        randomPlayer.Add(1, "Ghost");
        randomPlayer.Add(2, "RedEnemy");
        randomPlayer.Add(3, "GreenEnemy");
        
        
        
        // Will spawn enemies at the start of the game
        //_spawnCoroutine = StartCoroutine(SpawnEnemies());
        _whenToSpawn = Random.Range(1, 5); // lets select 1-5 secs to spawn tell when to spawn
    }

    private void Update()
    {
        // Let's increase spawn time by delta time
        _spawnTime += Time.deltaTime;

        // If our spawn time exceeds the threshold that was randomly generated then spwn

        if (_spawnTime > _whenToSpawn)
        {
            // Get the random enemy from dict before sending it into the function

            _randomEnemyIndex = Random.Range(1, randomPlayer.Count + 1);
            string id = randomPlayer[_randomEnemyIndex];
            
            SpawnEnemiesTimed(id);

            // reset the timer after spawning
            _spawnTime = 0;
            // Get the next threshold for spawning
            _whenToSpawn = Random.Range(1, 5);
        }
    }

    private void SpawnEnemiesTimed(string id)
    {
        if (IsAlive)
        {
            Debug.Log("EnemySpawner -- Timed Based");
        
            _randomSide = Random.Range(0, 2); // (0 -> 1)

            // Let's spawn the new enemy
        
            _newEnemy = EnemyPooler.Instance.SpawnFromEnemyPool(id);


            // after that put it left or right position

            // If 0 is selected then let's out it to the left side and if 1 is selected then let's spawn from right side

            if (_randomSide == 0) // spawn from left side
            {
                _newEnemy.transform.position = leftSide.position; // take the new enemy that is to be spawned to the our left tag position we placed in scene
                // lets set speed of enemy (will update the speed we made inside the Enemy movement
                _newEnemy.GetComponent<EnemyMovement>().speed = Random.Range(5, 10); // picking speed from the Enemy movement script and setting it
            }
            else // spawn from right side
            {
                _newEnemy.transform.position = rightSide.position;
                _newEnemy.GetComponent<EnemyMovement>().speed = -Random.Range(4,
                    11); // using (-) because the enemy from right side need to move in negative direction of x
                // we also need to flip the enemy when it (with sprite renderer)
                // we can also set scale of x to -1
                _newEnemy.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        else
        {
            Debug.Log("Player is Dead, Not More Spawning of Enemies!");
        }
    }
    
    
    // Event Subscription to stop Respawn
    private void OnEnable()
    {
        EventController.isPlayerDead += IsPlayerDead;
    }

    private void IsPlayerDead()
    {
        IsAlive = false;
    }
    private void OnDisable()
    {
        EventController.isPlayerDead -= IsPlayerDead;
    }
}