using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private Rigidbody2D shotBody;
    public float force;
    private GameObject player;
    private float timer;
    private float flyTime = 5f;
    void Start()
    {
        shotBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        Vector3 rotation = transform.position - player.transform.position;
        shotBody.velocity = new Vector2(direction.x, direction.y).normalized * force;
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
