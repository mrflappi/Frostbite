using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSetup : MonoBehaviour
{
    public GameManager gameManager;

    public void StartSetup()
    {
        SetupSpawnPoints();
        ChangeMaterial(gameManager.defaultChunkMat);
        SpawnDecals();
    }

    private void SetupSpawnPoints()
    {
        GameObject newSpawnPoint = Instantiate(gameManager.spawnPointPrefab, transform);
        SpawnPoint spawnPointScript = newSpawnPoint.GetComponent<SpawnPoint>();

        spawnPointScript.gameManager = gameManager;
        spawnPointScript.Setup();
    }

    private void ChangeMaterial(Material newMat)
    {
        MeshRenderer meshRenderer = transform.GetComponent<MeshRenderer>();
        meshRenderer.material = newMat;
    }

    private void SpawnDecals()
    {
        int randAmt = Random.Range(0, 3);
        for (int i = 0; i < randAmt; i++)
        {
            GameObject newDecal = Instantiate(gameManager.decals[Random.Range(0, gameManager.decals.Count)], transform);
            Vector2 randPos = new Vector2(
                Random.Range(-transform.localScale.x, transform.localScale.x),
                Random.Range(-transform.localScale.z, transform.localScale.z));

            float randScale = Random.Range(0.5f, 1.2f);

            Quaternion randRot = new Quaternion(0, 0, 0, 0);
                randRot.y = Random.Range(0, 360);

            newDecal.transform.localPosition = new Vector3(randPos.x, 0.01f, randPos.y);
            newDecal.transform.localScale = new Vector3(newDecal.transform.localScale.x * randScale, 1, newDecal.transform.localScale.z * randScale);
            newDecal.transform.rotation = randRot;
        }
    }
}
