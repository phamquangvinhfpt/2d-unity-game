using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float MAX_HEALTH = 100;
    [SerializeField] private float damage = 20;
    public float health { get; private set; }
    private Animator animator;
    private bool isDead = false;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private void Awake()
    {
        health = MAX_HEALTH;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            Damage(damage);
        }
        if (isDead)
        {
            animator.SetTrigger("Death");
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    public void Damage(float amount)
    {
        health -= amount;
        if (health > 0)
        {
            animator.SetTrigger("hurt");
            StartCoroutine(InvulnerbilityDuration());
        }
        else
        {
            if(!isDead)
            {
                health = 0;
                GetComponent<Boss01Movement>().enabled = false;
                isDead = true;
            }
            
        }
    }
    public void Heal(float amount)
    {
        float h = health + amount;
        if(h > MAX_HEALTH)
        {
            health = MAX_HEALTH;
        }
        else
        {
            health = h;
        }
    }

    private IEnumerator InvulnerbilityDuration()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        for(int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
