using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    
    // **************** Game Objects to Destroy ****************
    //private readonly string _enemyTag = "Enemy";
    private readonly string _playerTag = "Player";
    
    // Left and Right Destroyer has isTrigger Enabled
    // If enemy of player triggers it -> Destroy that game object
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GreenEnemy") || other.gameObject.CompareTag("RedEnemy") || other.gameObject.CompareTag("Ghost"))
        {
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag(_playerTag))
        {
            Destroy(other.gameObject);
        }
    }
}
