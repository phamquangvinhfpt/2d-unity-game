using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviement : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundedLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        //flip
        if (horizontal > 0.01f) 
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (horizontal < -0.01f) 
            transform.localScale = new Vector3(-1f, 1f, 1f);
        Animator();
    }

    void Jump()
    {
        if(isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            animator.SetTrigger("jump");
        } else if(onWall() && !isGrounded())
        {
            wallJumpCooldown = 0;
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
        }
    }

    void Animator()
    {
        //set animator parameters
        animator.SetBool("walk", Input.GetAxisRaw("Horizontal") != 0);
        animator.SetBool("grounded", isGrounded());

        //wall jump logic
        if (wallJumpCooldown < 0.2f)
        {
        
            body.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, body.velocity.y);

            if(onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else body.gravityScale = 7;

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else wallJumpCooldown += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundedLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
