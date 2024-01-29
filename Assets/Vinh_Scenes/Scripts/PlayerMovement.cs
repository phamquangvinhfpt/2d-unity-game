using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviement : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator animator;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * speed, body.velocity.y);
        //flip
        if (horizontal > 0.01f) transform.localScale = new Vector3(1f, 1f, 1f);
        else if (horizontal < -0.01f) transform.localScale = new Vector3(-1f, 1f, 1f);
        Animator();
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, speed);
            animator.SetTrigger("jump");
            isGrounded = false;
        }
    }
    
    void Animator()
    {
        animator.SetBool("walk", Input.GetAxisRaw("Horizontal") != 0);
        animator.SetBool("grounded", isGrounded);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
