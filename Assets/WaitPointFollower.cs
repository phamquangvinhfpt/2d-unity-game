using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class WaitPointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waitpoint;
    private int currentWaitPointIndex = 0;
    [SerializeField] private float speed = 2f;
    private SpriteRenderer spriteRenderer;
    private bool isFlipped = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Vector2.Distance(waitpoint[currentWaitPointIndex].transform.position, transform.position) < .1f)
        {
            currentWaitPointIndex++;
            if(currentWaitPointIndex >= waitpoint.Length)
            {
                currentWaitPointIndex = 0;
            }
            if (!isFlipped)
            {
                spriteRenderer.flipX = true;
                isFlipped = true;
            }
            else
            {
                spriteRenderer.flipX = false;
                isFlipped = false;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waitpoint[currentWaitPointIndex].transform.position, Time.deltaTime * speed);
        
    }
}
