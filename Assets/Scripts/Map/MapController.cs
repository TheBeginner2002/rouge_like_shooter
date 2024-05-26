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
    private Vector3 playerLastPosition;
    private PoolManager _poolManager;
    private string _chunkTag;
    

    public GameObject CurrentChunk
    {
        get => currentChunk;
        set => currentChunk = value;
    }

    private void Awake()
    {
        playerLastPosition = player.transform.position;
        _poolManager = GetComponent<PoolManager>();
    }

    private void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if (!currentChunk) return;
        
        Vector3 moveDir = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        string directionName = GetDirectionName(moveDir);

        CheckAndSpawnChunk(directionName);

        if (directionName.Contains("Up"))
        {
            CheckAndSpawnChunk("Up");
        }
        if (directionName.Contains("Down"))
        {
            CheckAndSpawnChunk("Down");
        }
        if (directionName.Contains("Right"))
        {
            CheckAndSpawnChunk("Right");
        }
        if (directionName.Contains("Left"))
        {
            CheckAndSpawnChunk("Left");
        }
    }

    void CheckAndSpawnChunk(string direction)
    {
        Vector3 spawnPosition = currentChunk.transform.Find(direction).position;

        if (!Physics2D.OverlapCircle(spawnPosition, checkerRadius, terrainMask) && !IsChunkAtPosition(spawnPosition))
        {
            SpawnChuck(currentChunk.transform.Find(direction).position);
        }
    }

    bool IsChunkAtPosition(Vector3 position)
    {
        foreach (var chunk in spawnedChunks)
        {
            if(Vector3.Distance(chunk.transform.position, position) < checkerRadius)
            {
                return true;
            }
        }
        return false;
    }

    string GetDirectionName(Vector3 direction)
    {
        direction = direction.normalized;

        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) 
        {
            if(direction.y > 0.5f) 
            {
                return direction.x > 0 ? "Right Up" : "Left Up";
            }
            else if(direction.y < -0.5f)
            {
                return direction.x > 0 ? "Right Down" : "Left Down";
            }
            else
            {
                return direction.x > 0 ? "Right" : "Left";
            }
        }
        else
        {
            if (direction.x > 0.5f)
            {
                return direction.y > 0 ? "Right Up" : "Right Down";
            }
            else if (direction.x < -0.5f)
            {
                return direction.y > 0 ? "Left Up" : "Left Down";
            }
            else
            {
                return direction.y > 0 ? "Up" : "Down";
            }
        }
    }

    void SpawnChuck(Vector3 spawnPosition)
    {
        int rand = Random.Range(0, terrainChunks.Count);
        _chunkTag = terrainChunks[rand].name;
        //_latestChunk = Instantiate(terrainChunks[rand], spawnPosition, Quaternion.identity);
        //spawnedChunks.Add(_latestChunk);

        _latestChunk = _poolManager.SpawnFromPool(_chunkTag, spawnPosition, Quaternion.identity);
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
        
        //foreach (var chunk in spawnedChunks)
        //{
        //    _opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
        //    if (_opDist > maxOpDist)
        //    {
        //        chunk.SetActive(false);
        //    }
        //    else
        //    {
        //        chunk.SetActive(true);
        //    }
        //}

        for(int i = spawnedChunks.Count - 1; i >= 0; i--)
        {
            var chunk = spawnedChunks[i];
            _opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (_opDist > maxOpDist)
            {
                _poolManager.ReturnToPool(_chunkTag,chunk);
                spawnedChunks.RemoveAt(i);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
