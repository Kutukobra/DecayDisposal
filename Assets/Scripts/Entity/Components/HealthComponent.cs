using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    Animator animator;
    public AudioSource hit_sound;
    public AudioSource death_sound;

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
        hit_sound.Play();
        animator?.SetTrigger("damaged");
        health -= value;
        isDead = health <= 0;
        animator?.SetBool("dead", isDead);

        if (isDead)
        {
            death_sound?.Play();
            Destroy(this.gameObject, 1f);
        }
    }

    public void Heal(float value)
    {
        health = health + value > maxHealth ? maxHealth : health + value;
    }
}
