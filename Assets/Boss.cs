using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private int damage;
    [SerializeField] private float range;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireball;
    [SerializeField] private float colliderDistance;
    private float coolDownTimer = Mathf.Infinity;

    [SerializeField]private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask layerMask;

    private Animator animator;

    private Health playerHealth;

    private BossPatrol bossPatrol;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        bossPatrol = GetComponentInParent<BossPatrol>();
    }
    private void Update()
    {
        coolDownTimer -= Time.deltaTime;
        if (PlayerInsight())
        {
            if(coolDownTimer >= attackCoolDown)
            {

                Attack();
            }
        }
        if(bossPatrol != null)
        {
            bossPatrol.enabled = !PlayerInsight();
        }
    }

    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x *range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, layerMask);

        if(hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
            
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));

    }

    private void DamagePlayer()
    {
        if (PlayerInsight())
        {
            playerHealth.Damage(damage);
        }
    }

    private void Attack()
    {
        coolDownTimer = 0;

        fireball[0].transform.position = firePoint.position;
        fireball[0].GetComponent<ProjectTile>().setDirection(Mathf.Sign(transform.localScale.x));
    }
}
