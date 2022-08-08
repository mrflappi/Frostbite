using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLight : MonoBehaviour
{
    public float torchPower;

    public float defaultTorchPower;
    public float percentTakenPerUpdate = 0.01f;

    public Light lightSource;
    private float intensity = new float();

    void Start()
    {
        torchPower = defaultTorchPower;
        intensity = lightSource.intensity;
        lightSource.intensity = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (lightSource.intensity > 0)
            {
                lightSource.intensity = 0;
            }
            else
            {
                lightSource.intensity = intensity;
            }
        }

    }

    void FixedUpdate()
    {
        if (lightSource.intensity > 0)
        {
            torchPower -= percentTakenPerUpdate;
        }
    }

}
