using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class BossPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("enemy")]
    [SerializeField] private Transform enemy;
    [SerializeField] private SpriteRenderer enemySprite;
    [Header("Movement Parameters")]
    [SerializeField] private float speed;

    private Vector3 initScale;
    private bool movingLeft;

    [SerializeField]private float idleTime;


    private float idleTimer;
    private void Awake()
    {
        initScale = enemy.localScale;
    }
    private void MoveInDirection(int direction)
    {
        idleTimer = 0;
        /*enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);*/
        enemy.position = new Vector3 (enemy.position.x + Time.deltaTime * direction * speed,
            enemy.position.y, enemy.position.z);
    }


    // Update is called once per frame
    void Update()
    {
        if(movingLeft)
        {
            if(enemy.position.x >= leftEdge.position.x)
            {
                enemySprite.flipX = true;
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
            
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                enemySprite.flipX=false;
                MoveInDirection(1);
            }

            else
            {
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {

        idleTimer += Time.deltaTime;
        if(idleTimer > idleTime)
        {
            movingLeft = !movingLeft;
        }
        
    }
}
