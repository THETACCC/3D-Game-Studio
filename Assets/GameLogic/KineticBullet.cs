using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticBullet : Bullet
{
    private void Start()
    {
        Invoke("DestroyWhenTooLong", bulletLifetime);
        GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }


}
