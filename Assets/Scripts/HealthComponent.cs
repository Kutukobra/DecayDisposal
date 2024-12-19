using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public bool isDead 
    {
        get;
        private set;
    } = false;

    public float maxHealth = 100;

    public float health 
    {
        get;
        private set;
    }

    public void Refill()
    {
        health = maxHealth;
    }
    
    public void TakeDamage(float value)
    {
        health -= value;

        isDead = health <= 0;
    }
}
