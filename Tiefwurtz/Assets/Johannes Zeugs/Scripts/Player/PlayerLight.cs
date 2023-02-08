using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Tiefwurtz
{
    public class PlayerLight : MonoBehaviour
    {
        [SerializeField] private Image currenthealthBar;

        public Light2D backLight;
        public Light2D playerLight;
        public GameObject GameManager;

        public float itemTouchBackLight = 1f;
        public float itemTouchLight = 1f;
        public float maxPlayerLight = 10f;
        public float maxBackLight = 5f;
        public float lightLossBack = 5f;
        public float lightLossPlayer = 5f;
        public bool keepLight;

        private GameManagerScribt gameManager;

        private float newBackLight;
        private float newPlayerLight;
        private float startBackIntensity;
        private float startPlayerIntensity;
        private float currentLight;
        private float currentPlayerLight;
        private bool refill = false;
        private bool refillPlayer = false;

        private void Start()
        {
            gameManager = GameManager.GetComponent<GameManagerScribt>();
            startBackIntensity = backLight.intensity;
            startPlayerIntensity = playerLight.intensity;
            currenthealthBar.fillAmount = 1f;
        }
        private void Awake()
        {
            currenthealthBar.fillAmount = 1f;
        }
        private void Update()
        {
            currenthealthBar.fillAmount = backLight.intensity / maxBackLight;

            if (playerLight.intensity < -0.5f)
                playerLight.intensity = -0.1f;

            if (backLight.intensity < -0.5f)
                backLight.intensity = -0.1f;

            RefillLight();
            RefillPlayerLight();
            OnDeath();

            if (keepLight)
                return;

            if (backLight.intensity > 0f)
            {
                backLight.intensity = backLight.intensity - (lightLossBack * 0.001f);

                if (playerLight.intensity < backLight.intensity)
                    return;

                if (playerLight.intensity < backLight.intensity + 20f && playerLight.intensity > backLight.intensity + 4f)
                {
                    playerLight.intensity = playerLight.intensity - (lightLossPlayer * 0.002f);
                }
                if (playerLight.intensity < backLight.intensity + 4f && playerLight.intensity > backLight.intensity + 3f)
                {
                    playerLight.intensity = playerLight.intensity - (lightLossPlayer * 0.001f);
                }
                if (playerLight.intensity < backLight.intensity + 3f && playerLight.intensity > backLight.intensity + 2f)
                {
                    playerLight.intensity = playerLight.intensity - (lightLossPlayer * 0.0008f);
                }
                if (playerLight.intensity < backLight.intensity + 2f)
                {
                    playerLight.intensity = playerLight.intensity - (lightLossPlayer * 0.0005f);
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Item")
            {
                currentLight = (backLight.intensity + startBackIntensity);
                currentPlayerLight = (playerLight.intensity + startPlayerIntensity);

                if (currentLight > maxBackLight)
                {
                    currentLight = maxBackLight - 0.1f;
                }
                if (currentPlayerLight > maxPlayerLight)
                {
                    currentPlayerLight = maxPlayerLight - 0.1f;
                }


                newBackLight = backLight.intensity + itemTouchBackLight;
                newPlayerLight = playerLight.intensity + itemTouchLight;


                refill = true;
                refillPlayer = true;


                if (backLight.intensity < 1f)
                {
                    backLight.intensity = 1f;
                }
                if (playerLight.intensity < 1f)
                {
                    playerLight.intensity = 1f;
                }
            }
        }
        private void RefillPlayerLight()
        {
            if (refillPlayer == true)
            {
                if (playerLight.intensity > newPlayerLight || playerLight.intensity > maxPlayerLight - 0.2f)
                    refillPlayer = false;

                if (playerLight.intensity < currentPlayerLight)
                {
                    if (playerLight.intensity < maxPlayerLight)
                    {
                        if (playerLight.intensity < newPlayerLight)
                                playerLight.intensity = playerLight.intensity + 0.05f;
                    }
                }
            }
        }
        private void RefillLight()
        {
            if (refill == true)
            {
                if (backLight.intensity > newBackLight || backLight.intensity > maxBackLight - 0.2f)
                    refill = false;

                if (backLight.intensity < currentLight)
                {
                    if (backLight.intensity < maxBackLight)
                    {
                        if(backLight.intensity < newBackLight)
                            backLight.intensity = backLight.intensity + 0.05f;
                    }
                }
            }
        }
        private void OnDeath()
        {
            if (backLight.intensity < 0.03)
            {
                gameManager.SetPlayerIsDead();
                gameManager.OnDeath();
            }
        }
    }
}