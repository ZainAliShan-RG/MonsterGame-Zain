using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Let's get the speed with which enemy will move
    [HideInInspector]
    public float speed;
    // Speed will be set from enemy spawner script where we make the clones of these

    private Rigidbody2D _enemyRb;
    
    // Start is called before the first frame update
    private void Awake()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _enemyRb.velocity = new Vector2(speed, _enemyRb.velocity.y);
    }
}
