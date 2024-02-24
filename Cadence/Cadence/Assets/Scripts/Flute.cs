using Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flute : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public GameObject flute;

    private float fireRate = 0.5f;
    private float fireCounter = 0f;
    private bool isShooting = false;


    private IPlayerController _player;
    void Awake()
    {
        _player = GetComponentInParent<IPlayerController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")&&_player.Grounded)
        {
            isShooting = true; // Player started shooting
        }

        // Toggle the visibility of the flute based on whether the player is shooting
        flute.GetComponent<Renderer>().enabled = isShooting;

        if (isShooting && Time.time > fireCounter)
        {
            Shoot();
            fireCounter = Time.time + fireRate;
        }

        if (Input.GetButtonUp("Fire1") || !_player.Grounded)
        {
            isShooting = false; // Player released the fire button
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
    }
}
