using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    Animator animator;

    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChange;
    public UnityEvent<bool> showCanvas;

    [SerializeField]
    private bool isInvincible = false; // trang thai bat bai ko bi dinh don
    private float timeSinceHit = 0;
    public float invincibilityTimer = 1f;

    // hien thi trang thai mau toi da tren unity cho de thuc thi
    [SerializeField]
    private int _maxHealth = 20;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    // hien thi trang thai con song tren unity
    [SerializeField]
    private bool _isAlive = true;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        private set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);

            //Debug.Log("is alive set " + value);
        }
    }


    // hien thi luong mau tren unity
    [SerializeField]
    private int _health;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            if (healthChange != null)
            {
                healthChange.Invoke(_health, MaxHealth); //Truyen 2 gia tri mau va mau toi da vao event
            }

            //check nv con mau ko?
            if (_health <= 0)
            {
                IsAlive = false;
                showCanvas.Invoke(!IsAlive);
            }
        }
    }

    public bool LockControl
    {
        get
        {
            return animator.GetBool(AnimationStrings.LockControl);
        }
        set
        {
            animator.SetBool(AnimationStrings.LockControl, value);
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTimer)
            {
                // roi trang thai bat bai
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;

        }

       
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            LockControl = true;

            animator.SetTrigger(AnimationStrings.isHit);

            if (damageableHit != null)
            {
                damageableHit.Invoke(damage, knockback);
            }
            //thong bao cho cac thanh phan khac co dung rang "damageable" da bi "hit" tu do xu ly knockback va v.v.

            

            return true;
        } else return false;
    }


    public void Heal (int healthRestore)
    {
        if (IsAlive)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int recoveryInPractice = Mathf.Min(maxHeal, healthRestore);
            Health += recoveryInPractice;
        }
    }
}
