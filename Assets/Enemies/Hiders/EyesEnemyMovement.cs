using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesEnemyMovement : MonoBehaviour
{
    public float force;
    public float maxSpeed;
    public Transform target;
    public MeshRenderer eyesMesh;
    private Rigidbody rb;

    private bool caught = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;

        rb.AddForce(transform.forward * force);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        if (caught == true)
        {
            eyesMesh.material.color = new Color(1, 1, 1, eyesMesh.material.color.a - 0.01f);
            Debug.Log(eyesMesh.material.color.a);
        }
        else if (eyesMesh.material.color.a < 1)
        {
            eyesMesh.material.color = new Color(1, 1, 1, eyesMesh.material.color.a + 0.05f);
        }

        if (eyesMesh.material.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyDissapear")
        {
            caught = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyDissapear")
        {
            caught = false;
        }
    }
}
