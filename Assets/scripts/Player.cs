using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Controller controller;
    float speedPerStep = .3f;
    float jumpHeight = .8f;
    float crouchHeight = -.4f;

    float speed = 0;
    float speedMod = 0.08f;
    float timeSinceLastStep = 0;
    float friction = .2f;
    
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

        timeSinceLastStep += Time.deltaTime;
        speed -= friction * speedMod;
        
        if (controller.steppedThisFrame){
            speed = 1 / timeSinceLastStep;
            timeSinceLastStep = 0;
        }

/*
        if (timeSinceLastStep > 1){
            speed = 0;
        }
*/

        if (speed <= 0)
            speed = 0;
        
        Vector3 newPosition = transform.position;
        newPosition.z += speed * speedMod;
        transform.position = newPosition;

        /*if (controller.steppedThisFrame){
            Vector3 newPosition = transform.position;
            if (controller.steppedThisFrame)
                newPosition.z += speedPerStep;
            transform.position = newPosition;
        }*/
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
