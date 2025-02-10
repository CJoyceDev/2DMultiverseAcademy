using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectyle : MonoBehaviour
{
    PlayerController pc;
    public bool abilityEnabled = false;

    public float bulletSpeed, fireRate, bulletDamage;
    public bool isAuto;

    public Transform bulletSpawnTransform;
    public GameObject bulletPrefab;

    private float timer;

   

    void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    
    void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }


    //Spawns a bullet in front of the player then applys a force = to bullet speed CJ
    //Need to use impulse in order to make the bullet accelerate quickly CJ
    public void ActivateAbility()
    {
        if (timer <= 0)
        {   
            
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnTransform.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnTransform.forward * bulletSpeed, ForceMode.Impulse);

            timer = 0.5f;
        }

    }
}
