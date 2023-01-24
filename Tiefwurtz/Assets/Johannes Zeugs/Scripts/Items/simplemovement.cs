using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplemovement : MonoBehaviour
{
    private float startY;
    private Rigidbody2D body;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(0f, 0.5f);
        startY = body.position.y;
    }

    
    void Update()
    {
        if (body.position.y < startY - 0.3f)
        {
            body.velocity = new Vector2(0f, 0.5f);
        }
        else if (body.position.y > startY + 0.3f)
        {
            body.velocity = new Vector2(0f, -0.5f);
        }
    }
}
