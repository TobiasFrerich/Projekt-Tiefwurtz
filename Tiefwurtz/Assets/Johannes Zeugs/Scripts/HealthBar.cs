using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tiefwurtz
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image HealthBarTotal;
        [SerializeField] private Image currenthealthBar;
        internal bool enabled; //health
        private float currentHealth;

        // Start is called before the first frame update
        void Start()
        {
            HealthBarTotal.fillAmount = currentHealth / 100;
        }

        void Update()
        {
            currenthealthBar.fillAmount = currentHealth / 100;             // 3 durch 10 für den 0.3
        }

        //health-system
        [SerializeField] private float startingHealth;
        //Accesss-Modifier, damit amn es auch an andere Script erkennen kann
        private Animator anim;
        private bool dead;
        internal int currentHealthP2;

        private void Awake()
        {
            currentHealth = startingHealth;
            anim = GetComponent<Animator>();
        }
        public void TakeDamage(float _damage)
        {
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);    // damit es nicht 0 wird und wir sollten nicht mehr Health haben wie am anfang des spiels. 

            if (currentHealth > 0)
            {
                anim.SetTrigger("hurtP1");
            }
            else
            {
                if (!dead)
                {
                    anim.SetTrigger("dieP1");
                    //GetComponent<player_movement>().enabled = false;
                    dead = true;
                    GetComponent<Collider2D>().enabled = false;
                }

            }

        }
    }
}
