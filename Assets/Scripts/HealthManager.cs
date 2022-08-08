using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int health;
    private int startHealth;
    private bool alive = true;

    public GameObject mainObject;
    public SkinnedMeshRenderer mesh;
    public ParticleSystem bloodParticles;

    [HideInInspector] public GameManager gameManager;

    public float secondsBeforeDespawn;

    void Start()
    {
        startHealth = health;
        mesh.enabled = false;
    }

    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(mainObject);
        }
    }

    public void Die()
    {
        transform.parent.GetComponent<EnemyMovement>().enabled = false;
        transform.GetComponent<ConstantForce>().enabled = false;
        alive = false;

        gameManager.score += 1;
        gameManager.enemyCount -= 1;

        StartCoroutine(Despawn(secondsBeforeDespawn));
    }

    public void TakeDamage(int damage, Vector3 hitPoint)
    {
        health -= damage;

        bloodParticles.transform.position = hitPoint;
        bloodParticles.Play();

        if (health <= 0 && alive == true)
        {
            Die();
        }
    }
    
    IEnumerator Despawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(mainObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyRender")
        {
            mesh.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyRender")
        {
            mesh.enabled = false;
        }
    }
}
