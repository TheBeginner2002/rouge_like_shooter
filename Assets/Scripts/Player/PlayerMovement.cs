using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Vector2 moveDir;

    [HideInInspector] public Vector2 lastMoveDir;
    
    private Rigidbody2D _rb;
    private PlayerStats _playerStats;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        lastMoveDir = new Vector2(1f, 0f);
    }

    private void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        InputMovement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InputMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastMoveDir = new Vector2(moveDir.x, 0);
        }
        
        if (moveDir.y != 0)
        {
            lastMoveDir = new Vector2(0, moveDir.y);
        }

        if (moveDir.y != 0 && moveDir.x != 0)
        {
            lastMoveDir = new Vector2(moveDir.x, moveDir.y);
        }
    }

    void Move()
    {
        _rb.velocity = new Vector2(moveDir.x, moveDir.y) * _playerStats.CurrentMoveSpeed;
    }
}
