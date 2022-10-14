using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    abstract public bool Stepped();
    abstract public bool Jumping();

    abstract public bool Crouching();
}
