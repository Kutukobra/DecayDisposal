using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class WeaponMelee : MonoBehaviour
{
    public float range = 0.5f;
    public float attackDamage = 10;
    public float knockbackForce = 0.5f;

    public int killCount = 0;
    
    public LayerMask effectLayer;

    private AudioSource hit;

    private HealthComponent healthComponent;

    public void Awake()
    {
        hit = GetComponent<AudioSource>();
        healthComponent = GetComponentInParent<HealthComponent>();
    }


    public void Attack()
    {

        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, range, effectLayer);

        foreach(var target in hitTargets)
        {
            hit?.Play();
            var healthStatus = target.gameObject.GetComponent<HealthComponent>();
            healthStatus.TakeDamage(attackDamage);
            target.gameObject.GetComponent<Knockback>().GetKnockback(transform, knockbackForce);

            healthComponent.Heal(attackDamage * 0.2f);

            if (healthStatus.isDead)
                killCount++;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
