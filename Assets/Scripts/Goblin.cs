using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDerection), typeof(Damageable))]

public class Goblin : MonoBehaviour
{
    public float walkSpeed = 3f;
    public DetectionZone atkDetectionZone;
    public DetectionZone cliffDetectionZone;

    Animator animator;
    Damageable damageable;

    Rigidbody2D rb;

    TouchingDerection touchingDerection;

    public enum WalkAbleDiraction { Right, Left}

    private WalkAbleDiraction _walkDiraction;
    private Vector2 walkDiractionVector = Vector2.right;

    public WalkAbleDiraction WalkDiraction
    {
        get { return _walkDiraction; }
        set 
        { 
            if (_walkDiraction != value)
            {
                // chuyen huong di
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkAbleDiraction.Right)
                {
                    walkDiractionVector = Vector2.right;
                } else
                {
                    walkDiractionVector = Vector2.left;
                }

            } 
            _walkDiraction = value; 
        }
    }

    public bool _haveTarget = false;

    public bool HaveTarget 
    {
        get { return _haveTarget; }
        private set 
        {
            _haveTarget = value;
            animator.SetBool(AnimationStrings.haveTarget, value);
         
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDerection = GetComponent<TouchingDerection>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }


    private void FixedUpdate()
    {
        if ((touchingDerection.IsOnWall || cliffDetectionZone.detectionCollider.Count == 0) && touchingDerection.IsGrounded)
        {
            //Debug.LogError(cliffDetectionZone.detectionCollider.Count);
            FlipDiraction();
        }

        if (CanMove && touchingDerection.IsGrounded)
        {
            rb.velocity = new Vector2(walkSpeed * walkDiractionVector.x, rb.velocity.y);
        }
        else
        {
            // nv chay cham lai tu rb.velocity.x ve 0 khi thaay muc tieu trong tam tan cong
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, 0.05f), rb.velocity.y);
        }
    }

    private void FlipDiraction()
    {
        if(WalkDiraction == WalkAbleDiraction.Right)
        {
            WalkDiraction = WalkAbleDiraction.Left;
        } else if (WalkDiraction == WalkAbleDiraction.Left) {
            WalkDiraction = WalkAbleDiraction.Right;
        } else
        {
            Debug.LogError("trang thai di chuyen kha dung chua dc thiet lap dung la Trai hoac Phai");
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HaveTarget = (atkDetectionZone.detectionCollider.Count > 0);

/*        if ((touchingDerection.IsOnWall || cliffDetectionZone.detectionCollider.Count == 0) && touchingDerection.IsGrounded)
        {
            //Debug.LogError(cliffDetectionZone.detectionCollider.Count);
            FlipDiraction();
        }

        if (CanMove && touchingDerection.IsGrounded)
        {
            rb.velocity = new Vector2(walkSpeed * walkDiractionVector.x, rb.velocity.y);
        }
        else
        {
            // nv chay cham lai tu rb.velocity.x ve 0 khi thaay muc tieu trong tam tan cong
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, 0.05f), rb.velocity.y);
        }*/
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

}
