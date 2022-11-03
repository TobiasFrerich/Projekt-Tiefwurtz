using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplemovement : MonoBehaviour
{
    private Rigidbody2D body;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(0, 1);
    }

    
    void Update()
    {
        if (body.position.y < -3f)
        {
            body.velocity = new Vector2(0, 1);
        }
        else if (body.position.y > -1f)
        {
            body.velocity = new Vector2(0, -1);
        }
    }
}
