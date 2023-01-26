using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyHealth = 100f;

    public GameObject item;
    public Transform itemTransform;
    public Color _color;
    public bool Dead = false;

    private void Update()
    {
        if (enemyHealth < 1f)
        {
            OnDeath();
        }
    }

    private IEnumerator Hurt()
    {
        Animator enemyAnim = GetComponent<Animator>();
        enemyAnim.SetBool("isHit", true);
        yield return new WaitForSeconds(0.45f);
        enemyAnim.SetBool("isHit", false);
    }

    private void OnDeath()
    {
        

        if (Dead == true)
                return;

        Animator enemyAnim = GetComponent<Animator>();
        enemyAnim.SetBool("isDead", true);
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Instantiate(item, itemTransform.position, Quaternion.identity);
        Dead = true;
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
