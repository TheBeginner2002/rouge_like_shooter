using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    [SerializeField] private float pullSpeed;
    
    private PlayerStats _playerStats;
    private CircleCollider2D _playerCollector;

    private void Start()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        _playerCollector = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        _playerCollector.radius = _playerStats.CurrentMagnet; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ICollectible collectible))
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDir = (transform.position - other.transform.position).normalized;
            rb.AddForce(forceDir * pullSpeed);
            
            collectible.Collect();
        }
    }
}
