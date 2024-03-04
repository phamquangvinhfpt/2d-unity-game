using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01Movement : MonoBehaviour
{
    private Rigidbody2D rd2d;
    private int jumpTime = 0;
    private float indexY;
    private Animator animator;
    private float dirX = 0f;
    private SpriteRenderer spriteRenderer;
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 7f;
    private BoxCollider2D Boxcoll;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask jumpableWall;

    [SerializeField] private float attackCoolDown;
    private float coolDownTimer = Mathf.Infinity;
    private enum MovementState { idle, walking, jump}
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        indexY = rd2d.velocity.y;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Boxcoll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rd2d.velocity = new Vector2(dirX * moveSpeed,rd2d.velocity.y);
        
        if (Input.GetButtonDown("Jump") && jumpTime < 2)
        {
            rd2d.velocity = new Vector2(rd2d.velocity.x, jumpForce);
            jumpTime++;
        }
        if (Input.GetKeyDown(KeyCode.J) && coolDownTimer > attackCoolDown)
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.H) && IsGrounded())
        {
            JumpAttack();
        }
        coolDownTimer += Time.deltaTime;
        if (indexY == rd2d.velocity.y)
        {
            jumpTime = 0;
        }
        UpdateAnimationUpdate();
    }
    private void UpdateAnimationUpdate()
    {
        MovementState state = MovementState.idle;
        if(dirX > 0f)
        {
            state = MovementState.walking;
            spriteRenderer.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.walking;
            spriteRenderer.flipX = true;
        }
        if (rd2d.velocity.y > .1f || rd2d.velocity.y < -.1f)
        {
            state = MovementState.jump;
        }
        animator.SetInteger("state", (int)state);
    }

    private bool onWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(Boxcoll.bounds.center, Boxcoll.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, jumpableWall);
        return hit.collider != null;
    }
    private void Attack()
    {
        coolDownTimer = 0;

        animator.SetTrigger("attack");

    }
    private void JumpAttack()
    {
        coolDownTimer = 0;
        rd2d.velocity = new Vector2(rd2d.velocity.x, jumpForce);
        animator.SetTrigger("JumpAttack");
        jumpTime++;
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(Boxcoll.bounds.center, Boxcoll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

}
