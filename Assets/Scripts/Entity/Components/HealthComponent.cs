using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        Refill();
        animator = GetComponent<Animator>();
    }

    public bool isDead 
    {
        get;
        private set;
    } = false;

    public float maxHealth = 100;

    [SerializeField]
    public float health;

    public void Refill()
    {
        health = maxHealth;
    }
    
    public void TakeDamage(float value)
    {
        animator?.SetTrigger("damaged");
        health -= value;
        isDead = health <= 0;
        animator?.SetBool("dead", isDead);
    }
}
