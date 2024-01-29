using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]private float attackCooldown;
    [SerializeField]private Transform firePoint;
    [SerializeField]private GameObject[] fireballs;
    private Animator animator;
    private PlayerMoviement playerMoviement;
    private float cooldownTimer = Mathf.Infinity;
    private 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMoviement = GetComponent<PlayerMoviement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMoviement.canAttack())
            Attack();
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return -1;
    }
}
