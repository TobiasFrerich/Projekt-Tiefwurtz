using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyHealth = 100f;

    public GameObject item;
    public Transform itemTransform;
    public SpriteRenderer _spriteRenderer;
    public Color _color;
    private void Update()
    {
        if (enemyHealth < 1f)
        {
            OnDeath();
        }
    }

    private IEnumerator Hurt()
    {
        Color SpriteColor = _spriteRenderer.color;
        _spriteRenderer.color = _color;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = SpriteColor;
    }

    private void OnDeath()
    {
        Destroy(gameObject);
        Instantiate(item, itemTransform.position, Quaternion.identity);
    }

    public void TakeDamage(float dmg)
    {
        if (enemyHealth > 1f)
        {
            StartCoroutine(Hurt());
        }
        enemyHealth = enemyHealth - dmg;
    }
}
