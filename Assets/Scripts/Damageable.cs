using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChange;
    // public GameObject winPanel;
    // public GameObject losePanel;
    // Start is called before the first frame update
    Animator animator;
    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth{
        get {
            return _maxHealth;
        }
        set {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;
    public int Health{
        get {
            return _health;
            }
        set {
            _health = value;
            healthChange?.Invoke(_health, MaxHealth);

            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }
    [SerializeField]
    bool isInvicible = false;

    // public bool IsHit { get{
    //     return animator.GetBool(AnimationStrings.isHit);
    // } private set{
    //     animator.SetBool(AnimationStrings.isHit, value);
    // } }
    public bool LockVelocity { get{
    return animator.GetBool(AnimationStrings.lockVelocity);
    }
    set{
        animator.SetBool(AnimationStrings.lockVelocity, value);
    }}
    [SerializeField]
    public bool _isAlive = true;
    private float timeSinceHit = 0;
    public float invicibilityTime = 0.25f;

    public bool IsAlive { get{return _isAlive;}
    set{
        _isAlive = value;
        animator.SetBool(AnimationStrings.isAlive, value);
        Debug.Log("isAlive set" + value);
    } }


    public void Awake()
    {
        animator = GetComponent<Animator>(); 
    }
    public void Update()
    {
        if (isInvicible)
        {
            if(timeSinceHit > invicibilityTime)
            {
                isInvicible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvicible)
        {
            Health -= damage;
            isInvicible = true;
            // IsHit = true;
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            return true;
        }
        // winPanel.SetActive(true);
        // Time.timeScale = 0;
        return false;
    }

    public bool Heal(int healthRestored)
    {
        if(IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHealth = Mathf.Min(maxHeal, healthRestored);
            Health += actualHealth;
            CharacterEvents.characterHealed(gameObject, actualHealth);
            return true;
        }
        return false;
    }
}
