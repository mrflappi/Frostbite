using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [HideInInspector] public GameManager gameManager;

    public void Setup()
    {
        gameManager.spawnPoints.Add(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameManager.spawnPoints.Remove(transform);
        }     
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.spawnPoints.Add(transform);
        }
    }
}
