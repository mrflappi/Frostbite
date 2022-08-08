using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        ConfigurableJoint joint = GetComponent<ConfigurableJoint>();
        float targetAngle = Mathf.Atan2(transform.position.z - player.position.z, transform.position.x - player.position.x);
        joint.targetRotation = Quaternion.Euler(90f, targetAngle, 0f);
    }
}
