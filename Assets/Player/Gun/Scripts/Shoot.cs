using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Camera cam;
    public ParticleSystem particles;

    public AudioSource audioSource;
    public List<AudioSource> audioBackups;

    public GameObject bulletHolePrefab;

    public int damage;
    public float force;
    public float range;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Pew();                
        }
    }

    public void Pew()
    {
        RaycastHit hit;
        
        GetComponent<Animator>().SetTrigger("Shoot");
        particles.Play();

        if (audioSource.isPlaying == true)
        {
            bool tryAgain = true;
     
            while (tryAgain == true)
            {
                AudioSource randomBackup = audioBackups[Random.Range(0, audioBackups.Count)];

                if (randomBackup.isPlaying == false)
                {
                    randomBackup.Play();
                    tryAgain = false;
                }
            }           
        }
        else 
        {
            audioSource.Play();
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Transform objHit = hit.transform;
            Debug.Log(objHit.name);

            if (objHit.GetComponent<LimbHealth>())
            {
                objHit.GetComponent<LimbHealth>().TakeDamage(damage, hit.point);
                if (objHit.GetComponent<Rigidbody>())
                {
                    objHit.GetComponent<Rigidbody>().AddForceAtPosition(force * cam.transform.forward, hit.point);
                }
            }
            else if (objHit.GetComponent<HealthManager>())
            {
                objHit.GetComponent<HealthManager>().TakeDamage(damage, hit.point);
                if (objHit.GetComponent<Rigidbody>())
                {
                    objHit.GetComponent<Rigidbody>().AddForceAtPosition(force * cam.transform.forward, hit.point);
                }
            }
            else
            {
                GameObject bulletHole = Instantiate(bulletHolePrefab, objHit.transform);

                Vector3 bulletHolePos = new Vector3();
                bulletHolePos = hit.point;
                bulletHolePos.y += 0.01f;

                bulletHole.transform.position = bulletHolePos;
            }
        }
    }
}
