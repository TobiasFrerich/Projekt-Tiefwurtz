using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathJar : MonoBehaviour
{
    private float maxFallSpeed = 5f;
    private Rigidbody2D body;
    private SpriteRenderer _spriteRenderer;
    public Sprite Jar;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_spriteRenderer.sprite == Jar)
        {
            if (body.velocity.y < -maxFallSpeed)
            {
                body.velocity = new Vector2(0, -maxFallSpeed);
            }
            else
            {
                body.velocity = new Vector2(0, body.velocity.y);
            }
        }
    }
}
