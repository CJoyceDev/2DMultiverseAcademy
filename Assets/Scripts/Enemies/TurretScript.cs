using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    

    public float bulletSpeed, fireRate;

    public Transform bulletSpawnTransform;
    public GameObject bulletPrefab;

    private float timer;



    void Start()
    {
       
    }


    void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            
        }

        if (timer <= 0)
        {
            fire();
        }

    }


    //Spawns a bullet in front of the player then applys a force = to bullet speed CJ
    //Need to use impulse in order to make the bullet accelerate quickly CJ
    public void fire()
    {
        if (timer <= 0)
        {

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnTransform.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnTransform.forward * bulletSpeed, ForceMode.Impulse);

            timer = fireRate;
        }

    }
}

