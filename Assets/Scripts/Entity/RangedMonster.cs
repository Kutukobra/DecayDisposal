using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonster : MonoBehaviour
{
    HealthComponent healthComponent;
    Animator animator;
    Rigidbody2D rigidBody;

    void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animator.SetBool("dead", healthComponent.isDead);
    }
}
