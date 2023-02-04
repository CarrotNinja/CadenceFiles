using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * author rash25
 * Last edited on 1/10/23
 * This program is a class that inherits from the InputController class, and overrides the abstract classes with user input
 */
[CreateAssetMenu(fileName ="PlayerController",menuName="InputController/PlayerController")]//Creates an asset in a folder in unity
public class PlayerController : InputController
{
    public override bool RetrieveJumpInput()
    {
        return Input.GetButtonDown("Jump");//overrides definition with jump input
    }
    public override float RetrieveMoveInput()
    {
        return Input.GetAxisRaw("Horizontal");//overrides definition with a/d input
    }
    public override bool RetrieveJumpHoldInput()//overrides definition with if the button is being held
    {
        return Input.GetButton("Jump");
    }
}
