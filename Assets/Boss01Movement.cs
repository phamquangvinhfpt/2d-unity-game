using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01Movement : MonoBehaviour
{
    private Rigidbody2D rd2d;
    private int jumpTime = 0;
    private float indexY;
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        indexY = rd2d.velocity.y;
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        rd2d.velocity = new Vector2(dirX * 7f,rd2d.velocity.y);

        if (Input.GetButtonDown("Jump") && jumpTime < 2)
        {
            rd2d.velocity = new Vector3 (0, 7f, 0);
            jumpTime++;
        }
        else
        {
            if(indexY == rd2d.velocity.y)
            {
                jumpTime = 0;
            }
        }
    }
}
