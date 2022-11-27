using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class Item : MonoBehaviour
{
    public Light2D light;
    private float startIntensity;

    private void Start()
    {
        startIntensity = light.intensity;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            light.intensity = startIntensity;
        }
    }
}
