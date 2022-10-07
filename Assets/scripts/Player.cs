using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Controller controller;
    float speedPerStep = .3f;
    float jumpHeight = .8f;
    float crouchHeight = -.4f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        Crouch();
    }

    void Run(){
        if (controller.steppedThisFrame){
            Vector3 newPosition = transform.position;
            if (controller.steppedThisFrame)
                newPosition.z += speedPerStep;
            transform.position = newPosition;
        }
    }

    void Jump(){
            Vector3 newPosition = transform.position;
            if (controller.jumping)
                newPosition.y = jumpHeight;
            else newPosition.y = 0;
            transform.position = newPosition;
    }

    void Crouch(){

        if (controller.jumping)
            return;
        
        Vector3 newPosition = transform.position;
        if (controller.crouching)
            newPosition.y = crouchHeight;
        else
            newPosition.y = 0;
        transform.position = newPosition;

    }
}
