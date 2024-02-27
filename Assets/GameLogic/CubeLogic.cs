using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    public Rigidbody rb;
    public float pushMagnitude = 1250f;

    void Start()
    {

    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GravityBullet")
        {
            Debug.Log("EnterTrigger");

            Vector3 directionFromBullet = transform.position - other.transform.position;
            directionFromBullet.Normalize();

            rb.AddForce(directionFromBullet * pushMagnitude);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "KineticBullet")
        {
            Debug.Log("EnterTrigger");

            Vector3 directionFromBullet = transform.position - other.transform.position;
            directionFromBullet.Normalize();

            rb.AddForce(-directionFromBullet * pushMagnitude);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "LiftBullet")
        {
            Debug.Log("EnterTrigger");


            rb.AddForce(Vector3.up * pushMagnitude);
            Destroy(other.gameObject);
        }
    }
}
