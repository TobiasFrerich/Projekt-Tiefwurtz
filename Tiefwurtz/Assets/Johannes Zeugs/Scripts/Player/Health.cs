using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
namespace Tiefwurtz
{
    public class Health : MonoBehaviour
    {
        public bool playerIsAlive = true;
        public GameObject GameManager;
        
        private GameManagerScribt gameManager;
        private float maxHealth;
        private float currentHealth;
        private CultistAttack cultAttack;
        private GameObject enemy;
        


        #region Main

        private void Start()
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
            currentHealth = maxHealth;
        }


        private void FixedUpdate()
        {
            //CheckIfPlayerAlive();
        }
        public void TakeDamage(float baseAmount)
        {
            if (baseAmount > 0f)
            {
                SetHealth(currentHealth - baseAmount);
            }
        }

        public void Heal(float amount)
        {
            if (amount > 0f)
            {
                SetHealth(currentHealth + amount);
            }
        }

        private void CheckIfPlayerAlive()
        {
            if (currentHealth < 0f)
            {
                playerIsAlive = false;
                //Death();
            }
        }

        private void SetHealth(float newHealth)
        {
            currentHealth = Mathf.Clamp(newHealth, 0f, maxHealth);
        }


        #endregion
    }
}
