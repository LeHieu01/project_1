using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 1;
    public Vector2 knockback = Vector2.zero;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 knockbackDirection = transform.parent.localScale.x > 0 ? knockback : new Vector2(knockback.x * -1, knockback.y);


            bool gotHit = damageable.Hit(attackDamage, knockbackDirection);

            if (gotHit)
            {
                Debug.Log(collision.name + " loss " + attackDamage + " hp");
            }
        }
    }


}
