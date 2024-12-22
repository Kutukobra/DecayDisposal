using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2 post;
    public float roamingRange;

    private bool allowTransition = true;

    private SpriteRenderer spriteRenderer;

    private Vector2 facingDirection;

    private enum State {
        Idle,
        Roaming,
        Chase,
        Attacking
    }

    private State currentState = State.Roaming;
    private Vector2 target;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = new Vector2();
    }

    void Update()
    {
        spriteRenderer.flipX = facingDirection.x < 0;
        
    }
}
