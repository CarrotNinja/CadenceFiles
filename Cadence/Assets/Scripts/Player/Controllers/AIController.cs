using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * author rash25
 * Last edited on 1/10/23
 * This program is a class that inherits from the InputController class, and overrides the abstract classes with AI inputs. The data right now is temporary just for testing.
 */
[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]//Creates an asset in a folder in unity

public class AIController : InputController
{
    public override bool RetrieveJumpInput()
    {
        return true;
    }
    public override float RetrieveMoveInput()
    {
        return 1f;
    }

    public override bool RetrieveJumpHoldInput()
    {
        return false;
    }
}
