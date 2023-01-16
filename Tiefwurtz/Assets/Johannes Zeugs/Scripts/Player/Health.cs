using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
namespace Tiefwurtz
{
    public class Health : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private float maxHealth = 100f;


        [Header("Debugging")]
        [SerializeField] private float currentHealth;

        public bool playerIsAlive = true;
        private CultistAttack cultAttack;
        private GameObject enemy;
        private Rigidbody2D playerbody;


        #region Main

        private void Start()
        {
            playerbody = GetComponent<Rigidbody2D>();
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
            playerbody.constraints = RigidbodyConstraints2D.FreezeAll;
            cultAttack = enemy.GetComponent<CultistAttack>();
            cultAttack.CheckIfPlayerIsAlive();
            Destroy(gameObject);
        }

        #endregion
    }
}
