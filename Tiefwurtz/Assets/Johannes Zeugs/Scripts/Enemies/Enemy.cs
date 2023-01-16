using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyHealth = 100f;

    public GameObject item;
    public Transform itemTransform;
    private void Update()
    {
        if (enemyHealth < 1f)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Destroy(gameObject);
        Instantiate(item, itemTransform.position, Quaternion.identity);
    }

    public void TakeDamage(float dmg)
    {
        enemyHealth = enemyHealth - dmg;
    }
}
