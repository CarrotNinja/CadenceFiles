using Controller;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health;
    public int damage;
    private float timeBtwDamage = 1.5f;

    private Animator anim;
    public Slider healthBar;
    public bool isDead;

    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=25)
        {
            anim.SetTrigger("stageTwo");
        }
        if (health <= 0)
        {
            anim.SetTrigger("death");
        }
        if(timeBtwDamage> 0)
        {
            timeBtwDamage -= Time.deltaTime;
        }
        healthBar.value = health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isDead == false)
        {
            if(timeBtwDamage<=0)
            {
                playerController.KBCounter = playerController.KBTotalTime;
                if (collision.transform.position.x <= transform.position.x)
                {
                    playerController.knockFromRight = true;
                }
                else
                {
                    playerController.knockFromRight = false;
                }
                playerController.TakeDamage(damage);
            }
        }
    }
}
