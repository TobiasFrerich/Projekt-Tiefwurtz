using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private Transform PlayerHitBox;
    private Rigidbody2D shotBody;
    private GameObject player;

    public float force;
    private float timer;
    private float flyTime = 5f;

    private Vector2 standing = new Vector2(0, 0);

    void Awake()
    {
        shotBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerHitBox = player.transform.Find("hitbox");
        if (player.GetComponent<Rigidbody2D>().velocity == standing)
        {
            Vector3 direction = player.transform.position - transform.position;
            Vector3 rotation = transform.position - player.transform.position;
            shotBody.velocity = new Vector2(direction.x, direction.y).normalized * force;
        }
        else
        {
            Vector3 direction = PlayerHitBox.position - transform.position;
            Vector3 rotation = transform.position - PlayerHitBox.position;
            shotBody.velocity = new Vector2(direction.x, direction.y).normalized * force;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > flyTime)
        {
            Destroy(gameObject);
            timer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
