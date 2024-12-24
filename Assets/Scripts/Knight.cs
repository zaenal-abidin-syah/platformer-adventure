using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class Knight : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;

    public float maxSpeed = 3f;
    public float walkAcceleration = 3f;
    public float walkStopRate = 0.6f;
    public enum WalkAbleDirection {right, left};
    public WalkAbleDirection _walkDirection;
    private Vector2 WalkDirectionVector = Vector2.right;
    TouchingDirections touchingDirections;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    Animator animator;
    Damageable damageable;

    public WalkAbleDirection WalkDirection {
        get {
            return _walkDirection;
        }
        set {
            if(_walkDirection != value){
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if(value == WalkAbleDirection.right){
                    WalkDirectionVector = Vector2.right;
                }else if(value == WalkAbleDirection.left){
                    WalkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }
    public bool _hasTarget = false;

    public bool HasTarget { 
        get{ return _hasTarget;
        }
    private set{
        _hasTarget = value;
        animator.SetBool(AnimationStrings.hasTarget, value);
    } }

    public bool CanMove {
        get{
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown { get{
        return animator.GetFloat(AnimationStrings.attackCooldown);
    } set{
        animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(0, value));
    } }

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    public void FixedUpdate()
    {
        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if(!damageable.LockVelocity)
        {
            if (CanMove && touchingDirections.IsGrounded){
                
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + (walkAcceleration * WalkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), rb.velocity.y);
            }else{
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }
    }
    private void FlipDirection()
    {
        if(WalkDirection == WalkAbleDirection.right){
            WalkDirection = WalkAbleDirection.left;
        }else if(WalkDirection == WalkAbleDirection.left){
            WalkDirection = WalkAbleDirection.right;
        }else{
            Debug.LogError("current walkable direction is not set to legal value of right or left");
        }
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0 ;
        if (AttackCooldown > 0){
            AttackCooldown -= Time.deltaTime;
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        damageable.LockVelocity = true;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    // public void OnNoGroundDetected()
    // {

    // }
    public void OnCliffDetected()
    {
        if(touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}
