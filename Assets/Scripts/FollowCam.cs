using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowCam : MonoBehaviour
{
    // We need to align camera's transform with players transform
    
    public Transform _playerTransform = null; // Getting transform of player
    private Vector3 _tempPosition; // To hold temp pos for cam

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    
    // check if player is found when scene loads
    
    // Start is called before the first frame update
// void Start()
// {
//     Debug.Log("Camera is working!!");
//     _playerTransform = GameObject.FindWithTag("Player").transform;
//     
//     Debug.Log("Found: " + _playerTransform);
// 
    public void AssignPlayer(Transform playerTransform)
    {
        _playerTransform = playerTransform;
        Debug.Log("Player assigned directly: " + _playerTransform.name);
    }
    private void LateUpdate()
    {
        Debug.Log("Late Update Called!"); 
        // Check if player available so that camera can follow it (in case player is dead)
        if (_playerTransform == null)
            return;

        
        _tempPosition = transform.position; // Store the cam's current position inside Temporary vector
        // Update temporary 3d vector's X with player's current position
        _tempPosition.x = _playerTransform.position.x; // store player current X position into temporary vector's x
        
        // Check for the bounds to not let camera go outside that, // If player's current pos on x is going less and our min bound, keep the cam at that current pos, don't let it go outside
        if (_tempPosition.x < minX)
            _tempPosition.x = minX;
        if (_tempPosition.x > maxX)
            _tempPosition.x = maxX;
        
        // Now update the cam's transform  
        transform.position = _tempPosition;
    }
}
