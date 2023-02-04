using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * author rash25
 * Last edited 2/2/23
 * Class to handle player animation
 */
public class AnimationHandaler : MonoBehaviour
{
    public Animator animator;//Refrence to an animator
    public Rigidbody2D rb;//Refrence to a rigidbody
    public Ground ground;//Refrene to ground script
    void Awake(){//On awake, set the components
        animator= GetComponent<Animator>();
        ground= GetComponent<Ground>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()//Update animation paramaters every frame
    {
        animator.SetBool("OnGround", ground.OnGround);
        animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("VelocityY", Mathf.Abs(rb.velocity.y));
    }
}
