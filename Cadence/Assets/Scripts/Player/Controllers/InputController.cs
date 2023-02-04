using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * author rash25
 * Last edited on 1/10/23
 * This is an abstract class.ScriptableObject makes it independent from GameObjects. Abstract key word allows for a common base class that classes can override definitons upon.
 */
public abstract class InputController : ScriptableObject 
{
    public abstract float RetrieveMoveInput();
    public abstract bool RetrieveJumpInput();

    public abstract bool RetrieveJumpHoldInput();
}
