using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
/*
 * author rash25
 * Last edited on 1/10/23
 * This class controls all things to do with ground, and retrieving friction from the material they are standing on.
 */

public class Ground : MonoBehaviour
{
    public bool OnGround { get; private set; }//Field that has a public get, but its set it private to check if onGround
    public float Friction { get; private set; }//Field that has a public get, but its set it private to have a multiplier for friction physics

    private PhysicsMaterial2D _material;//Creates a private Physics Unity Material.

    private void OnCollisionEnter2D(Collision2D collision)//When the player enters a collision
    {
        EvaluateCollision(collision);//Evaluate the collision
        RetrieveFriction(collision);//Retrieve the friction given the enviorment
    }
    private void OnCollisionStay2D(Collision2D collision)//When the player stays in a collision
    {
        EvaluateCollision(collision);//Evaluate the collision
        RetrieveFriction(collision);//Retrieve the friction given the enviorment.
    }

    private void OnCollisionExit2D(Collision2D collision)//When the player exits a collision
    {
        OnGround= false;//Set grounded to false
        Friction= 0;//and set friction to 0
    }

    private void EvaluateCollision(Collision2D collision)
    {
        for(int i = 0; i < collision.contactCount; i++)//For every contact of collision
        {
            Vector2 normal = collision.GetContact(i).normal;//Creates a vector that is the normal of that specifc contact.
            OnGround |= normal.y >= 0.9f;//If any of the normals are vertically down from the player, set grounded to true.
        }
    }

    private void RetrieveFriction(Collision2D collision)
    {
        _material = collision.rigidbody.sharedMaterial;//Sets the physics material to the material of the thing collided with

        Friction = 0;//Set friction to 0

        if(_material != null)//If there is a material in the gameObject collided with
        {
            Friction = _material.friction;//Set the friction accordingly
        }

    }

    public bool GetOnGround() { return OnGround; }//Basic public method to return OnGround
    public float GetFriction() { return Friction; }//Basic public method to return friction
    
}
