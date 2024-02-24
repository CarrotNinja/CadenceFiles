using Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int damage;
    public PlayerController playerController;
    public void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("Player"))
        {
            playerController.KBCounter = playerController.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerController.knockFromRight= true;
            }
            else
            {
                playerController.knockFromRight= false;
            }
            playerController.TakeDamage(damage);
        }
    }
}
