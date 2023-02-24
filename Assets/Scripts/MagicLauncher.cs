using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLauncher : MonoBehaviour
{
    public GameObject magicBulletPrefab;
    public Transform fireLocation;

    public void FireMagic()
    {
        GameObject magicBullet = Instantiate(magicBulletPrefab, fireLocation.position, magicBulletPrefab.transform.rotation);
        Vector2 magicBulletScale = magicBullet.transform.localScale;
        
        float isFacingRight;
        if (transform.localScale.x > 0)
        {
            isFacingRight = 1;
        } else
        {
            isFacingRight = -1;
        }

        magicBullet.transform.localScale = new Vector2(magicBulletScale.x * System.Convert.ToSingle(isFacingRight), magicBulletScale.y);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
