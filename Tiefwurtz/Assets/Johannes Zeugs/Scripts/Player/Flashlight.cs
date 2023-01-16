using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
namespace Tiefwurtz
{
    public class Flashlight : MonoBehaviour
    {
        public Light2D light;
        private GameObject enemy;
        private CultistAttack cultAttack;
        public float maxLightHealth = 15;
        public float lightLoss = 5f;

        private float startIntensity;
        private bool refill = false;
        private float currentLight;

        private void Start()
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
            startIntensity = light.intensity;
        }

        private void Update()
        {
            RefillLight();
            OnDeath();
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

                if (currentLight > maxLightHealth)
                {
                    currentLight = maxLightHealth - 0.1f;
                }

                refill = true;

                if (light.intensity < 1f)
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
                    if (light.intensity < maxLightHealth)
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
        private void OnDeath()
        {
            if (light.intensity < 0.03)
            {
                cultAttack = enemy.GetComponent<CultistAttack>();
                cultAttack.SetPlayerIsNotAlive();
                Destroy(gameObject);
            }
        }
    }
}