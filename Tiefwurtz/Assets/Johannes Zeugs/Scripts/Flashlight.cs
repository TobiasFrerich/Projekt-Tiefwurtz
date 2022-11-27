using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    public Light2D light;
    private float startIntensity;
    public float lightLoss = 5;

    private void Start()
    {
        startIntensity = light.intensity;
    }

    void Update()
    {
        if (light.intensity > 0)
        {
            light.intensity = light.intensity - (lightLoss * 0.01f);
        }
    }

    public void ResetLight()
    {
        light.intensity = startIntensity;
    }
}