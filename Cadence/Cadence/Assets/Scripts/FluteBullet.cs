using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluteBullet : MonoBehaviour
{
    public float speed = 15f;
    public Rigidbody2D rb;
    public float frequency = 15f;
    public float magnitude = 0.7f;

    private float startTime;

    private Vector3 pos;
    private Vector3 axis;

    private void Awake()
    {
        startTime = Time.time;
    }
    void Start()
    {
        pos = transform.position;
        Destroy(gameObject, 0.4f);
        axis = transform.up;
    }
    void Update()
    {
        pos += transform.right * Time.deltaTime * speed;
        transform.position = pos + axis * Mathf.Sin((Time.time-startTime) * frequency) * magnitude;
    }
}
