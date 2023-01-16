using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
namespace Tiefwurtz
{
    public class Health : MonoBehaviour
    {
        private float maxHealth;
        private float currentHealth;
        public bool playerIsAlive = true;
        private CultistAttack cultAttack;
        private GameObject enemy;


        #region Main

        private void Start()
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
            currentHealth = maxHealth;
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

        private void SetHealth(float newHealth)
        {
            currentHealth = Mathf.Clamp(newHealth, 0f, maxHealth);
            if (currentHealth <= 0f)
            {
                playerIsAlive = false;
                StartDeath();
            }
        }

        private void StartDeath()
        {
            cultAttack = enemy.GetComponent<CultistAttack>();
            cultAttack.SetPlayerIsNotAlive();
            Destroy(gameObject);
        }

        #endregion
    }
}
