using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
namespace Tiefwurtz
{
    public class Flashlight : MonoBehaviour
    {
        public Light2D backLight;
        public Light2D playerLight;
        private GameObject enemy;
        private CultistAttack cultAttack;
        public float maxLightHealth = 15;
        public float lightLossBack = 5f;
        public float lightLossPlayer = 5f;
        public bool keepLight;

        private float startBackIntensity;
        private float startPlayerIntensity;
        private bool refill = false;
        private float currentLight;

        private void Start()
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
            startBackIntensity = backLight.intensity;
            startPlayerIntensity = playerLight.intensity;
        }

        private void Update()
        {
            if (keepLight)
                return;

            if (backLight.intensity > 0f)
            {
                backLight.intensity = backLight.intensity - (lightLossBack * 0.001f);

                playerLight.intensity = playerLight.intensity - (lightLossPlayer * 0.001f);
            }

            RefillLight();
            OnDeath();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Item")
            {
                currentLight = (backLight.intensity + startBackIntensity);

                if (currentLight > maxLightHealth)
                {
                    currentLight = maxLightHealth - 0.1f;
                }

                refill = true;

                if (backLight.intensity < 1f)
                {
                    backLight.intensity = 1f;
                    playerLight.intensity = 1f;
                }
            }
        }
        private void RefillLight()
        {
            if (refill == true)
            {
                if (backLight.intensity < currentLight)
                {
                    if (backLight.intensity < maxLightHealth)
                    {
                        backLight.intensity = backLight.intensity + 0.05f;
                        playerLight.intensity = playerLight.intensity + 0.05f;
                    }
                }
                else if (backLight.intensity > (currentLight - 1))
                {
                    refill = false;
                }
            }
        }
        private void OnDeath()
        {
            if (backLight.intensity < 0.03)
            {
                cultAttack = enemy.GetComponent<CultistAttack>();
                cultAttack.SetPlayerIsNotAlive();
                Destroy(gameObject);
            }
        }
    }
}