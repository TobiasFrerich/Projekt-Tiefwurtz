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
        body.velocity = new Vector2(0, 1);
        startY = body.position.y;
    }

    
    void Update()
    {
        if (body.position.y < startY - 1.5f)
        {
            body.velocity = new Vector2(0, 1);
        }
        else if (body.position.y > startY + 1.5f)
        {
            body.velocity = new Vector2(0, -1);
        }
    }
}
