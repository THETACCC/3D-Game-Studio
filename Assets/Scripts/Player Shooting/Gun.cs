using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject firingPoint;
    [SerializeField]
    private float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            SlowShoot();
        }
    }

    public void Shoot()
    {
        Debug.Log("ShooTING");
        GameObject bullet = Instantiate(bulletPrefab, firingPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        Destroy(bullet, 5f);
    }

    public void SlowShoot()
    {
        Debug.Log("ShooTING");
        GameObject bullet = Instantiate(bulletPrefab, firingPoint.transform.position, transform.rotation);
        bullet.gameObject.transform.localScale = Vector3.one;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed / 4);
        Destroy(bullet, 5f);
    }

}
