using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private AudioClip damageSoundClip;

    public int health = 100;
    // Start is called before the first frame update
    public void TakeDamage(int damage)
    {
        health -= damage;
        //SoundFXManager.instance.playSoundFXClip(damageSoundClip, transform, 1f);
        SoundFXManager.instance.playSoundFXClip(damageSoundClip, transform, 1f);
        if (health <= 0)
        {
            Die();
        }        
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
