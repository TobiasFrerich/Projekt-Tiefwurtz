using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using Cinemachine;
namespace Tiefwurtz
{
    public class Flashlight : MonoBehaviour
    {
        public Light2D backLight;
        public Light2D playerLight;
        public GameObject GameManager;


        public float maxPlayerLight = 10f;
        public float maxBackLight = 5f;
        public float lightLossBack = 5f;
        public float lightLossPlayer = 5f;
        public bool keepLight;

        private GameManagerScribt gameManager;
        private GameObject enemy;
        private CultistAttack cultAttack;
        private SpriteRenderer _spriteRenderer;

        private float startBackIntensity;
        private float startPlayerIntensity;
        private float currentLight;
        private float currentPlayerLight;
        private bool refill = false;
        private bool refillPlayer = false;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            gameManager = GameManager.GetComponent<GameManagerScribt>();
            enemy = GameObject.FindGameObjectWithTag("Enemy");
            cultAttack = enemy.GetComponent<CultistAttack>();
            startBackIntensity = backLight.intensity;
            startPlayerIntensity = playerLight.intensity;
        }

        private void Update()
        {
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

                if (currentLight > maxBackLight)
                {
                    currentLight = maxBackLight - 0.1f;
                }

                refill = true;

                if (backLight.intensity < 1f)
                {
                    backLight.intensity = 1f;
                }
            }
            if (other.gameObject.tag == "Item")
            {
                currentPlayerLight = (playerLight.intensity + startPlayerIntensity);

                if (currentPlayerLight > maxPlayerLight)
                {
                    currentPlayerLight = maxPlayerLight - 0.1f;
                }

                refillPlayer = true;

                if (playerLight.intensity < 1f)
                {
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
                    if (backLight.intensity < maxBackLight)
                    {
                        backLight.intensity = backLight.intensity + 0.05f;
                    }
                }
                else if (backLight.intensity > (currentLight - 1))
                {
                    refill = false;
                }
            }
        }
        private void RefillPlayerLight()
        {
            if (refillPlayer == true)
            {
                if (playerLight.intensity < currentPlayerLight)
                {
                    if (playerLight.intensity < maxPlayerLight)
                    {
                        playerLight.intensity = playerLight.intensity + 0.05f;
                    }
                }
                else if (playerLight.intensity > (currentPlayerLight - 1))
                {
                    refillPlayer = false;
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