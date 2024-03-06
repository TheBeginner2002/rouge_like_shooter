using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }

    public List<Drops> drops;

    private void OnDestroy()
    {
        if(!this.gameObject.scene.isLoaded) return;
        float randNum = Random.Range(0f, 100f);
        List<Drops> possibleDrops = new List<Drops>();
        foreach (var rate in drops)
        {
            if (randNum <= rate.dropRate)
            {
                possibleDrops.Add(rate);
                
            }
        }
        //check possible drop
        if (possibleDrops.Count >= 0)
        {
            Drops drop = possibleDrops[Random.Range(0, possibleDrops.Count)];
            Instantiate(drop.itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
