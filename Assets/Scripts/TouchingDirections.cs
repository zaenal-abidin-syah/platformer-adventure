using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    CapsuleCollider2D touchingCol;

    [SerializeField]
    public bool _isOnWall;
    public bool IsOnWall { get{
        return _isOnWall;
    } private set{
        _isOnWall = value;
        animator.SetBool(AnimationStrings.isOnWall, value);
    } }

    [SerializeField]
    public bool _isOnCeiling;
    public bool IsOnCeiling { get{
        return _isOnCeiling;
    } private set{
        _isOnCeiling = value;
        animator.SetBool(AnimationStrings.isOnCeiling, value);
    } }

    [SerializeField]
    public bool _isGrounded;
    public bool IsGrounded { get{
        return _isGrounded;
    } private set{
        _isGrounded = value;
        animator.SetBool(AnimationStrings.isGrounded, value);
    } }

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;

    }
}
