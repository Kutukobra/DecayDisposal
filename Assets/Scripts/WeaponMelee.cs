using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class WeaponMelee : MonoBehaviour
{
    public float range = 0.5f;
    public float attackDamage = 10;
    public float attackSpeed = 2f;
    
    public LayerMask effectLayer;

    public float nextAttackTime = 0;

    public void Attack()
    {
        if (Time.time < nextAttackTime)
            return;
        
        nextAttackTime = Time.time + (1f / attackSpeed);

        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, range, effectLayer);

        foreach(var target in hitTargets)
        {
            Debug.Log("Hit " + target.name);
            target.gameObject.GetComponent<HealthComponent>().TakeDamage(attackDamage);
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
