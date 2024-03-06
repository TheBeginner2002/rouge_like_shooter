using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    private MapController _mapController;

    [SerializeField] private GameObject targetMap;

    private void Awake()
    {
        _mapController = FindObjectOfType<MapController>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _mapController.CurrentChunk = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _mapController.CurrentChunk == targetMap)
        {
            _mapController.CurrentChunk = null;
        }
    }
}
