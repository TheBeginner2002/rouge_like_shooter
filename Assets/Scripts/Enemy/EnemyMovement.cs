using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform _player;

    private EnemyStats _enemyStats;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Start()
    {
        _enemyStats = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, _player.position, _enemyStats.CurrentSpeed * Time.deltaTime);
    }
}
