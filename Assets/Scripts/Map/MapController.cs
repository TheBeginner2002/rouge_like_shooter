using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> terrainChunks;
    [SerializeField] private GameObject player;
    [SerializeField] private float checkerRadius;
    [SerializeField] private LayerMask terrainMask;
    [SerializeField] private GameObject currentChunk;
    
    [Header("Optimization")]
    [SerializeField] List<GameObject> spawnedChunks;
    [SerializeField] private float maxOpDist;
    [SerializeField] private float optimizerCooldownDur;
    private GameObject _latestChunk;
    private float _optimizerCooldown;
    private float _opDist;
    

    public GameObject CurrentChunk
    {
        get => currentChunk;
        set => currentChunk = value;
    }

    private Vector3 _noTerrainPosition;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if (!currentChunk) return;
        
        if (_playerMovement.moveDir is { x: > 0, y: 0 }) //right
        {
            CallSpawnChunk("Up");
            CallSpawnChunk("Right");
            CallSpawnChunk("Down");
        }
        else if (_playerMovement.moveDir is { x: < 0, y: 0 }) //left
        {
            CallSpawnChunk("Up");
            CallSpawnChunk("Left");
            CallSpawnChunk("Down");
        }
        else if (_playerMovement.moveDir is { x: 0, y: > 0 }) //up
        {
            CallSpawnChunk("Right");
            CallSpawnChunk("Up");
            CallSpawnChunk("Left");
        }
        else if (_playerMovement.moveDir is { x: 0, y: < 0 }) //down
        {
            CallSpawnChunk("Right");
            CallSpawnChunk("Down");
            CallSpawnChunk("Left");
        }
        else if (_playerMovement.moveDir is { x: > 0, y: > 0 }) //right up
        {
            CallSpawnChunk("Up");
            CallSpawnChunk("Right Up");
            CallSpawnChunk("Right");
        }
        else if (_playerMovement.moveDir is { x: > 0, y: < 0 }) //right down
        {
            CallSpawnChunk("Right");
            CallSpawnChunk("Right Down");
            CallSpawnChunk("Down");
        }
        else if (_playerMovement.moveDir is { x: < 0, y: > 0 }) //left up
        {
            CallSpawnChunk("Left");
            CallSpawnChunk("Left Up");
            CallSpawnChunk("Up");
        }
        else if (_playerMovement.moveDir is { x: < 0, y: < 0 }) //left down
        {
            CallSpawnChunk("Left");
            CallSpawnChunk("Left Down");
            CallSpawnChunk("Down");
        }
    }

    void CallSpawnChunk(String nameDir)
    {
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(nameDir).position, checkerRadius, terrainMask))
        {
            _noTerrainPosition = currentChunk.transform.Find(nameDir).position;
            SpawnChuck();
        }
    }

    void SpawnChuck()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        _latestChunk = Instantiate(terrainChunks[rand], _noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(_latestChunk);
    }

    void ChunkOptimizer()
    {
        _optimizerCooldown -= Time.deltaTime;
        
        if (_optimizerCooldown <= 0f)
        {
            _optimizerCooldown = optimizerCooldownDur;
        }
        else return;
        
        foreach (var chunk in spawnedChunks)
        {
            _opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (_opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
