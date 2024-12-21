using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    HealthComponent healthComponent;

    void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    void Update()
    {
        
    }
}
