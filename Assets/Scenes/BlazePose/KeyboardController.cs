using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : Controller
{

    public override bool Stepped(){
        if (Input.GetKeyDown("w"))
            return true;
            else return false;
        
    }

    public override bool Jumping()
    {
        if (Input.GetKeyDown("space"))
            return true;
            else return false;
    }

    public override bool Crouching()
    {
        if (Input.GetKey("s"))
            return true;
            else return false;
    }
}
