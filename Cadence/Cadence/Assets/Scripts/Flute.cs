using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flute : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    private float fireRate = 0.5f;
    private float canFire = 1f;
    private void Update()
    {
        if(Input.GetButton("Fire1")&&Time.time>canFire)
        {
            Shoot();
            canFire = Time.time+fireRate;
        }
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
    }
}
