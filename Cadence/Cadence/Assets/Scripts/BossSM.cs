using Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BossSM : StateMachine
{
    [HideInInspector] public IdleDecide idle;
    [HideInInspector] public JumpAttack jump;
    [HideInInspector] public RollAttack roll;

    [SerializeField]
    public GameObject player;
    public PlayerController playerController;
    [SerializeField]
    public Rigidbody2D rigidBody;
    [SerializeField]
    public SpriteRenderer spr;

    public HealthBar healthBar;

    public int damage = 20;


    [SerializeField] public int decideDist;
    [SerializeField] public int jumpAttackHeight;
    [SerializeField] public int rollSpeed;


    public int health;
    public int maxHealth;

    public bool facingRight ;

    public GameObject wave1;
    public GameObject wave2;
    public GameObject wave3;
    public void Awake()
    {
        this.idle = new IdleDecide(this);
        this.jump = new JumpAttack(this);
        this.roll = new RollAttack(this);
        facingRight= true;
        maxHealth = health;
        healthBar.SetMaxHealth(maxHealth);

    }

    protected override BaseState GetFirstState()
    {
        return this.idle;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            Die();
        }
    }


    void FlipTowardsPlayer()
    {
        float playerPos = player.transform.position.x - transform.transform.position.x;
        if ((playerPos < 0 && facingRight)|| (playerPos > 0 && !facingRight))
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        spr.flipX = !spr.flipX;
    }


    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Destroy(gameObject);
        
    }

    private void FixedUpdate()
    {
        FlipTowardsPlayer();
        if (health <= maxHealth * 0.75f && health>maxHealth*0.5f)
        {
            wave1.SetActive(true);
        }else if (health <= maxHealth * 0.5f && health > maxHealth * 0.25)
        {
            wave2.SetActive(true);
        }else if (health <= maxHealth * 0.25f)
        {
            wave3.SetActive(true);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
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
