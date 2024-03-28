using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluteBullet : MonoBehaviour
{
    public AudioClip[] fluteSounds;
    public float speed = 15f;
    public Rigidbody2D rb;
    public float frequency = 15f;
    public float magnitude = 1f;
    public int damage = 50;

    private float startTime;

    private Vector3 pos;
    private Vector3 axis;

    private void Awake()
    {
        startTime = Time.time;
    }
    void Start()
    {
        SoundFXManager.instance.playRandomSoundFXClip(fluteSounds, transform, 0.6f);
        pos = transform.position;
        Destroy(gameObject, 0.4f);
        axis = transform.up;
    }
    void Update()
    {
        pos += transform.right * Time.deltaTime * speed;
        transform.position = pos + axis * Mathf.Sin((Time.time-startTime) * frequency) * magnitude;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();
        BossSM boss = collision.GetComponent<BossSM>();
        if(monster != null )
        {
            monster.TakeDamage(damage);
        }else if(boss!=null)
        {
            boss.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
