using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    public Light2D light;
    public float maxLight = 15;
    public float lightLoss = 5f;

    private float startIntensity;
    private bool refill = false;
    private float currentLight;

    private void Start()
    {
        startIntensity = light.intensity;
    }

    void Update()
    {
        RefillLight();

        if (light.intensity > 0f)
        {
            light.intensity = light.intensity - (lightLoss * 0.01f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            currentLight = (light.intensity + startIntensity);

            if (currentLight > maxLight)
            {
                currentLight = maxLight - 0.1f;
            }

            refill = true;

            if(light.intensity < 1f)
            { 
                light.intensity = 1f;
            }
        }
    }
    private void RefillLight()
    {
        if (refill == true)
        {           
            if (light.intensity < currentLight)
            {
                if (light.intensity < maxLight)
                {
                    light.intensity = light.intensity + 0.05f;
                }
            }
            else if (light.intensity > (currentLight - 1))
            {
                refill = false;
            }
        }
    }
}