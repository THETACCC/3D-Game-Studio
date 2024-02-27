using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBullet : Bullet
{
    private void Start()
    {
        Invoke("DestroyWhenTooLong", bulletLifetime);
        GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Interactions")
        {
            Rigidbody colRigidbody =  collision.gameObject.GetComponent<Rigidbody>();
            colRigidbody.AddForce(-transform.forward * 100);
            Destroy(gameObject);
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactions")
        {
            Debug.Log("EnterTrigger");
            Rigidbody colRigidbody = other.gameObject.GetComponent<Rigidbody>();
            colRigidbody.AddForce(-transform.up * 50);
            Destroy(gameObject);
        }
    }
    */
}
