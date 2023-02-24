using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 2;

    public AudioSource healthPuckupAudio;

    private void Awake()
    {
        healthPuckupAudio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            damageable.Heal(healthRestore);
            if (healthPuckupAudio != null)
            {
                AudioSource.PlayClipAtPoint(healthPuckupAudio.clip, gameObject.transform.position, healthPuckupAudio.volume);
            }
            Destroy(gameObject);
        }
    }
}
