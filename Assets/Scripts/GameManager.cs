using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform player;

    [Header("Enemies")]
    public List<GameObject> enemyGameObjects;    
    [HideInInspector] public List<Transform> spawnPoints;
    public Transform enemyParent;
    public int maxEnemies;
    [HideInInspector] public int enemyCount;
    public GameObject spawnPointPrefab;

    [Header("Chunks")]
    public Material defaultChunkMat;

    [Header("UI")]
    public Text scoreText;
    public int score;

    [Header("Ambience")]
    public List<AudioClip> ambienceSounds;
    public AudioSource ambienceSource;

    [Header("Decals")]
    public List<GameObject> decals;
    public float decalRange;
    public int maxDecalsPerChunk;

    void Start()
    {
        StartCoroutine(RandomAmbience());
    }

    void Update()
    {
        scoreText.text = score.ToString();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnZombos()
    {
        for (; ;) {
            yield return new WaitForSeconds(Random.Range(2, 10));

            if (enemyCount < maxEnemies)
            {
                GameObject newEnemy = Instantiate(enemyGameObjects[Random.Range(0, enemyGameObjects.Count)], enemyParent);

                EnemyMovement enemyMovement = newEnemy.transform.GetChild(0).GetComponent<EnemyMovement>();
                LookAtTarget enemyHead = enemyMovement.head;
                Animator enemyAnim = enemyMovement.animator;

                enemyMovement.player = player;
                enemyMovement.gameManager = this;

                enemyHead.player = player;

                enemyAnim.speed = Random.Range(0.5f, 2);

                bool spawnPointAvailable = false;
                Transform spawnPoint = null;

                while (spawnPointAvailable == false)
                {
                    spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                    
                    if (spawnPoint.gameObject.activeInHierarchy)
                    {
                        spawnPointAvailable = true;
                    }
                }

                newEnemy.transform.position = spawnPoint.position;

                enemyCount += 1;
            }          
        }
    }

    IEnumerator RandomAmbience()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(Random.Range(20, 80));
            if(ambienceSource.isPlaying == false)
            {
                ambienceSource.clip = ambienceSounds[Random.Range(0, ambienceSounds.Count)];
                ambienceSource.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                ambienceSource.Play();
            }        
        }       
    }
}
