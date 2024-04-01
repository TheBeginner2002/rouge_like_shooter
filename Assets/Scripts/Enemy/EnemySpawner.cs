using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public List<EnemyGroup> enemyGroups;
        public string waveName;
        public int waveQuota; //The total number of enemies in this wave
        public float spawnInterval; // The interval at which to spawn
        public int spawnCount; // The number of enemies already spawned in this wave
    }
    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; //number enemy spawn in this wave
        public int spawnCount; //number of this type enemy has spawned in this wave
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; //list of all wave in game
    public int currentWaveCount; //the index current wave[start with 0]
    
    private Transform player;
    
    [Header("Spawner Attributes")] 
    private float spawnTimer;// Time to use determine when spawn next enemy
    public float waveInterval;// The interval each wave
    public int enemiesAlive;//
    public int maxEnemiesAllowed;//maximum enemies allowed on map at once
    public bool maxEnemiesReached = false;// check number of enemies is reaching max? 

    [Header("Spawn Positions")] 
    public List<Transform> relativeSpawnPoint;

    IEnumerator BeginNextWave()
    {
        //Wave for `waveInterval` before starting the next wave
        yield return new WaitForSeconds(waveInterval);
        //If there are more wave to start after current wave, move on to next wave
        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CaculateWaveQuota();
        }
    }
    
    void CaculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        
        // Debug.LogWarning(currentWaveQuota);
    }

    void SpawnEnemies()
    {
        // check if the minimum number of enemies in the wave has been spawned
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            // spawn each type of enemy until the quota is filled
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                // check if minimum number of enemies of this type have been spawned
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    // Limit the number of enemies that can be spawned at once
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    Instantiate(enemyGroup.enemyPrefab,
                        relativeSpawnPoint[Random.Range(0, relativeSpawnPoint.Count)].position,
                        Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }

        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    public void OnEnemyDied()
    {
        enemiesAlive--;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CaculateWaveQuota();
    }

    private void Update()
    {
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0)
        {
            StartCoroutine(BeginNextWave());
        }
        spawnTimer += Time.deltaTime;
        // check if time to spawn enemy
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }
}
