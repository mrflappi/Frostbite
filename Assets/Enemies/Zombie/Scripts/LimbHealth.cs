using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbHealth : MonoBehaviour
{
    public HealthManager healthManager;
    private ParticleSystem bloodParticles;

    public int limbHealth;
    public float damageDampening;

    public bool lockX;
    public bool lockY;
    public bool lockZ;

    private List<Transform> children = new List<Transform>();

    void Start()
    {
        bloodParticles = healthManager.bloodParticles;
    }

    void Update()
    {
        if (healthManager.health <= 0)
        {
            GoLimp();
        }
    }
    public void TakeDamage(int damage, Vector3 hitPoint)
    {
        limbHealth -= damage;

        healthManager.TakeDamage(Mathf.RoundToInt(damage / damageDampening), hitPoint);

        if (limbHealth <= 0)
        {
            GoLimp();
        }
    }

    public void GoLimp()
    {
        if (GetComponent<ConfigurableJoint>())
        {
            ConfigurableJoint joint = GetComponent<ConfigurableJoint>();

            JointDrive xDrive = joint.angularXDrive;
            JointDrive yZDrive = joint.angularYZDrive;

            xDrive.positionSpring = 0;
            yZDrive.positionSpring = 0;

            joint.angularXDrive = xDrive;
            joint.angularYZDrive = yZDrive;

            if (lockX) { joint.angularXMotion = ConfigurableJointMotion.Locked; }
            if (lockY) { joint.angularYMotion = ConfigurableJointMotion.Locked; }
            if (lockZ) { joint.angularZMotion = ConfigurableJointMotion.Locked; }

            if (joint.transform.GetComponent<CopyLimb>())
            {
                joint.transform.GetComponent<CopyLimb>().enabled = false;
            }
        }

        ConfigurableJoint[] childJoints = GetComponentsInChildren<ConfigurableJoint>();
        foreach(ConfigurableJoint joint in childJoints)
        {
            if (joint.GetComponent<LimbHealth>())
            {
                JointDrive xDrive = joint.angularXDrive;
                JointDrive yZDrive = joint.angularYZDrive;

                xDrive.positionSpring = 0;
                yZDrive.positionSpring = 0;

                joint.angularXDrive = xDrive;
                joint.angularYZDrive = yZDrive;

                joint.angularXMotion = ConfigurableJointMotion.Limited;
                joint.angularYMotion = ConfigurableJointMotion.Limited;
                joint.angularZMotion = ConfigurableJointMotion.Limited;

                if (joint.transform.GetComponent<CopyLimb>())
                {
                    joint.transform.GetComponent<CopyLimb>().enabled = false;
                }
            }           
        }
    }
}
