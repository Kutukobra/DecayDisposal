using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;

    [SerializeField]
    private Vector2 direction;
    private Vector2 facingDirection;
    private Vector2 facingDirectionAttacking;

    [SerializeField]
    private Vector2 mousePosition;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    public Transform weaponParent;
    public float attackSpeed = 2f;
    public float nextAttackTime = 0;

    public WeaponMelee weapon;

    bool isMoving = false;
    bool isAttacking = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get Axis Direction;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        direction.Normalize();

        isMoving = direction != Vector2.zero;
        animator?.SetBool("Moving", isMoving);

        facingDirection = GetFacingDirection();
        spriteRenderer.flipX = facingDirection.x < 0;
        animator.SetFloat("yFacing", facingDirection.y);
        animator.SetFloat("xFacing", Mathf.Abs(facingDirection.x));

        weaponParent.right = facingDirection;

        // Attack Cooldown
        isAttacking = Time.time < nextAttackTime;

        if (Input.GetMouseButton(0))
        {            
            Attack();
        }      
    }

    void FixedUpdate()
    {
        UpdatePosition();
    }

    void Attack()
    {
        // Guard Clause Cooldown.
        if (isAttacking)
            return;

        nextAttackTime = Time.time + (1f / attackSpeed);

        facingDirectionAttacking = facingDirection;

        animator.SetTrigger("Attack");
        weapon.Attack();
    }

    Vector2 GetFacingDirection()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 facing = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;

        return isAttacking ? facingDirectionAttacking : facing;
    }

    private void UpdatePosition()
    {
        rigidBody.velocity = direction * movementSpeed;
    }
}
