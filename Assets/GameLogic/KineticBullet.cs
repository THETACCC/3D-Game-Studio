using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticBullet : Bullet
{
    private void Start()
    {
        Invoke("DestroyWhenTooLong", bulletLifetime);
        Init(bulletSpeed,false);
    }




    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "interactions")
        {
            Destroy(gameObject);
        }

    }


}
