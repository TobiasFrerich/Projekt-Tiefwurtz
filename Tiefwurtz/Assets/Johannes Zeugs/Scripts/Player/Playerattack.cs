using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{
    public class Playerattack : MonoBehaviour
    {
        [SerializeField] float attackRange = 0.5f;

        public SpriteRenderer _spriteRenderer;
        public Sprite attackSprite;
        public Sprite normalSprite;
        public Transform attackpoint;
        public LayerMask enemyLayers;

        private Enemy enemyHealth;
        private void Start()
        {
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(Attack());
            }
        }

        private IEnumerator Attack()
        {
            _spriteRenderer.sprite = attackSprite;
            yield return new WaitForSeconds(0.1f);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                enemyHealth = enemy.GetComponent<Enemy>();
                enemyHealth.TakeDamage(20f);
            }

            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.sprite = normalSprite;
        }
    }
}
