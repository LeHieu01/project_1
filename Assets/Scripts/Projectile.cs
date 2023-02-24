using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int magicDamage = 2;
    public float magicBulletSpeed = 4f;
    public Vector2 knockback = new Vector2(2, 2);

    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        // vì đạn bắng thẳng nên sử dụng kinematic mode thay vì dynamic de ko bị tác động của trọng lực
        rb.velocity = new Vector2(transform.localScale.x * magicBulletSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 knockbackDirection = transform.localScale.x > 0 ? knockback : new Vector2(knockback.x * -1, knockback.y);


            bool gotHit = damageable.Hit(magicDamage, knockbackDirection);

            if (gotHit)
            {
                Debug.Log(collision.name + " loss " + magicDamage + " hp");
                Destroy(gameObject);
            }
        } 
    }
}
