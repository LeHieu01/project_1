using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public float flightSpeed = 3f;
    private float waypointReachedDistance = 0.1f;
    public DetectionZone atkDetectionZone;
    public List<Transform> waypoints;

    Animator animator;
    Damageable damageable;
    Rigidbody2D rb;

    Transform nextWaypoint;
    int waypointNum = 0;

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
    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
     void Update()
    {
        HaveTarget = (atkDetectionZone.detectionCollider.Count > 0);
    }

    private void Flight()
    {
        //Bay den waypoint
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        //check waypoint
        
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

        //Kiem tra xem co can doi waypoint khong
        if (distance <= waypointReachedDistance)
        {
            //Doi waypoint khac
            waypointNum++;

            if (waypointNum >= waypoints.Count)
            {
                //Loop back ve waypoint ban dau
                waypointNum = 0;
            }
            nextWaypoint = waypoints[waypointNum];
        }
    }
    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;
        if (transform.localScale.x > 0)
        {
            //nhin` sang phai
            if (rb.velocity.x <= 0)
            {
                //Lat mat
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            //nhin` sang trai
            if (rb.velocity.x > 0)
            {
                //Lat mat
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        
        }
    }
 

    /*public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }*/
}

