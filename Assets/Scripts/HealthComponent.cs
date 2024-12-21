using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    void Awake()
    {
        Refill();
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
        Debug.Log("Taking damage.");
        health -= value;

        isDead = health <= 0;
    }
}
