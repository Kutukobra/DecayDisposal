using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class WeaponMelee : MonoBehaviour
{
    public float range = 0.5f;
    public float attackDamage = 10;
    
    public LayerMask effectLayer;


    public void Attack()
    {

        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, range, effectLayer);

        foreach(var target in hitTargets)
        {
            Debug.Log("Hit " + target.name);
            target.gameObject.GetComponent<HealthComponent>().TakeDamage(attackDamage);
            target.gameObject.GetComponent<Knockback>().GetKnockback(transform, 1/range * attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
