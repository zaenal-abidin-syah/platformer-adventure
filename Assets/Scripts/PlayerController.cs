using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float walkSpeed = 5f;
    public float airWalkSpeed = 3f;
    public float runSpeed = 8f;
    private float jumpImpulse = 10f;
    TouchingDirections touchingDirections;
    Damageable damageable;

    public float currentMoveSpeed{
        get{
            if(CanMove){
                if (IsMoving && !touchingDirections.IsOnWall){
                    if (touchingDirections.IsGrounded)
                    {
                        if(IsRunning){
                            return runSpeed;
                        }
                            return walkSpeed;
                    
                    }else{
                        return airWalkSpeed;
                    }
                    
                }else{
                    return 0f;
                }
            }else{
                return 0f;
            }
        }
                
    }
    Vector2 moveInput;
    Rigidbody2D rb;
    [SerializeField]
    bool _isMoving = false;
    Animator animator;
    

    public bool IsMoving { 
        get {
            return _isMoving;
        } 
        private set{
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
        
    }
    [SerializeField]

    private bool _isRunning = false;
    public bool IsRunning {
        get {
            return _isRunning;
        }
        private set {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }    
    }
    public bool _isFacingRight = true;

    public bool CanMove {get {
        return animator.GetBool(AnimationStrings.canMove);
    }}

    public bool  IsAlive{
        get{
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }
    

    public bool IsFacingRight { get{return _isFacingRight;} 
        private set {
            if (_isFacingRight != value){
                transform.localScale *= new Vector2(-1,1);
            }
            _isFacingRight = value;
        } 
        }



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }


    private void FixedUpdate()
    {
        if(!damageable.LockVelocity)
        {
        rb.velocity = new Vector2(moveInput.x * currentMoveSpeed, rb.velocity.y);
        }
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if(IsAlive){
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }else{
            IsMoving = false;
        }
        
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight){
            IsFacingRight = true;
        }else if(moveInput.x < 0 && IsFacingRight){
            IsFacingRight = false;
        }
    }

    // onRun
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }else if (context.canceled)
        {
            IsRunning = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangeAttackTrigger);
        }
    }
}
