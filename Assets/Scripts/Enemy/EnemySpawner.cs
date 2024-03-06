using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public List<GameObject> enemyPrefabs; //List of enemy prefabs for this wave
        public string waveName;
        public int waveQuota; //The total number of enemies in this wave
        public float spawnInterval; // The interval at which to spawn
        public int spawnCount; // The number of enemies already spawned in this wave
    }
    
    
}
