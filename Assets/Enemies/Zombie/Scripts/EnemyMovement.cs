using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public ConfigurableJoint hipJoint;
    public HealthManager healthManager;
    private Rigidbody hipRB;
    public LookAtTarget head;
    public Animator animator;
    [HideInInspector] public Transform player;
    [HideInInspector] public GameManager gameManager;

    public float minSpeed;
    public float maxSpeed;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        hipRB = healthManager.GetComponent<Rigidbody>();
        healthManager.gameManager = gameManager;
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        Accelerate();
        float targetAngle = Mathf.Atan2(hipRB.position.z - player.position.z, hipRB.position.x - player.position.x) * Mathf.Rad2Deg;
        hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle + 90, 0f);
    }

    public void Accelerate()
    {
        hipRB.AddForce(hipRB.transform.forward * speed);
    }
}
