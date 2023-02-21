using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using System.Collections;

namespace Tiefwurtz
{
    public class PlayerLight : MonoBehaviour
    {
        [SerializeField] private AudioSource itemCollect;

        [SerializeField] private ParticleSystem GetLightParticals;

        public Light2D backLight;

        public static float backLightIntensity;

        public float itemTouchBackLight = 1f;
        public float maxBackLight = 5f;
        public float lightLossBack = 5f;
        public bool keepLight;

        private GameObject GameManager;
        private GameManagerScribt gameManagerScr;
        private GameObject StartPoint;

        private float Timer;
        public Color black;
        public Image AKey;
        public Image DKey;
        public Image SPACEKey;
        public GameObject AnfangsTExt;
        public GameObject LightExplain;
        public GameObject AttackExplain;
        private bool Tutorial;
        private float newBackLight;
        public float startBackIntensity;
        private float currentLight;
        private bool refill = false;
        public static bool reachedACheckpoint = false;
        public static Vector3 currentSavePoint;
        

        private void Start()
        {
            StartPoint = GameObject.FindGameObjectWithTag("StartPoint");
            GameManager = GameObject.FindGameObjectWithTag("GameManager");
            gameManagerScr = GameManager.GetComponent<GameManagerScribt>();
            if (backLightIntensity > 0f)
            {
                backLight.intensity = backLightIntensity;
            }
            if(!reachedACheckpoint)
            {
                Tutorial = true;
                currentSavePoint = StartPoint.transform.position;
                transform.position = currentSavePoint;
                backLight.intensity = 10f;
            }
            else
            {
                transform.position = currentSavePoint;
            }
            startBackIntensity = backLight.intensity;
        }
        private void Update()
        {
            if (Time.timeScale == 0)
                return;

            if(Tutorial)
            {
                AnfangsTExt.SetActive(true);

                if (Input.GetKeyDown(KeyCode.A))
                {
                    AKey.color = black;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    DKey.color = black;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SPACEKey.color = black;
                }

                if (AKey.color == black && SPACEKey.color == black && DKey.color == black)
                {
                    AnfangsTExt.SetActive(false);
                    LightExplain.SetActive(true);
                    Timer += 1 * Time.deltaTime;
                    if (Timer > 5f)
                    {
                        LightExplain.SetActive(false);
                        AttackExplain.SetActive(true);
                        if (Input.GetMouseButtonDown(0))
                        {
                            AttackExplain.SetActive(false);
                            Tutorial = false;
                        }
                    }
                }

            }


            backLightIntensity = backLight.intensity;

            if (backLight.intensity < -0.5f)
                backLight.intensity = -0.1f;

            RefillLight();

            if(!gameManagerScr.playerIsDead)
            {
                OnDeath();
            }

            if (keepLight)
                return;

            if (backLight.intensity > 0f)
            {
                backLight.intensity = backLight.intensity - (lightLossBack * 0.001f);
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Item")
            {
                GetLightParticals.Play();
                ParticleSystem.EmissionModule em = GetLightParticals.emission;
                em.enabled = true;
                itemCollect.Play();
                currentLight = (backLight.intensity + startBackIntensity);

                if (currentLight > maxBackLight)
                {
                    currentLight = maxBackLight - 0.1f;
                }

                newBackLight = backLight.intensity + itemTouchBackLight;

                refill = true;

                if (backLight.intensity < 1f)
                {
                    backLight.intensity = 1f;
                }
            }
            if(other.tag == "Checkpoint")
            {
                reachedACheckpoint = true;
                currentSavePoint = other.transform.position;
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
            if (backLight.intensity < 0.03 && !gameManagerScr.playerIsDead)
            {
                gameManagerScr.SetPlayerIsDead();
                gameManagerScr.OnDeath();
            }
        }
        
        
    }
}