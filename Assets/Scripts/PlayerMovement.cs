using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{
    // **************** Inspector Attributes ****************
    
    [SerializeField] private float moveSpeed = 10f; // With this speed player will walk
    [SerializeField] private float jumpForce = 14f; // With this speed/force jump will happen

    [SerializeField] private Rigidbody2D player1Rb; // To Get Player's Rigid Body

    [SerializeField] private Animator _animator; // To Get Animator Applied on the Player

    [SerializeField] private SpriteRenderer sr; // for flipping purpose

    // **************** String Tags **************************
    
    private readonly string _isWalking = "isWalking";

    private readonly string _groundTag = "Ground";
    
    
    // ***************** Helping Vars *************************

    private bool _isGrounded; // check if player is at ground

    private int _isWalkingHash;

    private float _movementX;
    
    // ***************** Audios *************************
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip jumpSound;

    
    // Methods Here
    
    private void Start()
    {
        _isWalkingHash = Animator.StringToHash(_isWalking);
    }

    private void Update()
    {
        _movementX = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {   
        KeyBoardControls();
        AnimatePlayer();
        JumpPlayer();
    }

    // Function to move the player on x-axis
    private void KeyBoardControls()
    {
        transform.position += new Vector3(_movementX, 0f, 0f) * (moveSpeed * Time.fixedDeltaTime);
    }

    private void AnimatePlayer()
    {
        switch(_movementX) 
        {
            case > 0: // means player is moving in right (+ve x) direction
                _animator.SetBool(_isWalkingHash, true); // activate the walk animation
                sr.flipX = false; // don't flip the player
                break;
            case < 0: // If moving left
                _animator.SetBool(_isWalkingHash, true); // enable walk animation
                sr.flipX = true; // flip tha player to look in -ve X direction
                break;
            case 0: // If Idle
                _animator.SetBool(_isWalkingHash, false); //Turn off Walk Animation
                break;
            default:
                print("Animation Failed!");
                break;
        }
    }
    
    private void JumpPlayer()
    {
        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            _isGrounded = false;
            audioSource.PlayOneShot(jumpSound);
            Debug.Log("Jump Pressed");
            player1Rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // If players collides with ground
        if (other.gameObject.CompareTag(_groundTag))
        {
            _isGrounded = true;
        }
        // If Green and Red Players Collide with Player
        if (other.gameObject.CompareTag("GreenEnemy") || other.gameObject.CompareTag("RedEnemy"))
        {
            if(EventController.PlayDeathSound != null)
                EventController.PlayDeathSound.Invoke();
            if (EventController.isPlayerDead != null)
                EventController.isPlayerDead.Invoke();
            Destroy(gameObject);
        }
    }

    // When player triggers with enemies that have trigger instead of collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ghost")) // if player triggers with ghost or enemy with trigger enabled
        {
            if(EventController.PlayDeathSound != null)
                EventController.PlayDeathSound.Invoke();
            if (EventController.isPlayerDead != null)
                EventController.isPlayerDead.Invoke();
            Destroy(gameObject);
        }
    }
}
