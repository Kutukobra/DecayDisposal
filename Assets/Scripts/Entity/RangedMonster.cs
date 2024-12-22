using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonster : MonoBehaviour
{
    HealthComponent healthComponent;
    Animator animator;
    Rigidbody2D rigidBody;

    public SpriteRenderer sprite;

    public Transform target;

    private Vector2 targetDirection;

    void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        targetDirection = (target.position - transform.position).normalized;
        sprite.flipX = targetDirection.x < 0;


        animator.SetBool("dead", healthComponent.isDead);
    }
}
