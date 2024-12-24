using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

public class RangedMonster : MonoBehaviour
{
    HealthComponent healthComponent;
    Animator animator;
    Rigidbody2D rigidBody;

    private SpriteRenderer sprite;

    [SerializeField]
    private Projectile projectile;

    public Transform target;

    public Transform projectileSpawnPoint;

    private Vector2 targetDirection;

    private RangedWeapon weapon;

    public float attackSpeed;
    private float nextAttackTime;

    void Awake()
    {
        target = GameObject.Find("Player").transform; 

        healthComponent = GetComponent<HealthComponent>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        weapon = GetComponent<RangedWeapon>();
    }

    void Update()
    {
        if (target == null)
            return;
            
        targetDirection = (target.position - projectileSpawnPoint.position).normalized;
        sprite.flipX = targetDirection.x < 0;

        animator.SetBool("dead", healthComponent.isDead);

        if (Vector2.Distance(target.position, transform.position) < 10)
            Shoot();
    }

    void Shoot()
    {
        if (Time.time < nextAttackTime)
            return;        

        nextAttackTime = Time.time + 1 / attackSpeed;

        weapon.Shoot(targetDirection);
        animator.SetTrigger("attacking");
    }
}
