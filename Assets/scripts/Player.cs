using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Controller controller;
    float speedPerStep = .3f;
    float jumpHeight = 5f;
    float crouchHeight = -.4f;

    float speed = 0;
    float speedCap = .12f;
    float speedMod = 0.08f;
    float timeSinceLastStep = 0;
    float friction = .2f;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<BodyController>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        Crouch();
        

    }

    void OnCollisionEnter(Collision trigger){
        speed = 0;
        Vector3 newPosition = transform.position;
        newPosition.z -= 5;
        transform.position = newPosition;
    }

    void Run(){

        timeSinceLastStep += Time.deltaTime;
        speed -= friction * speedMod;
        
        if (controller.Stepped()){
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

        float finalSpeed = speed * speedMod;
        
        if (finalSpeed > speedCap)
            finalSpeed = speedCap;
        
        print (finalSpeed);
        Vector3 newPosition = transform.position;
        newPosition.z += finalSpeed;
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
            if (controller.Jumping())
                newPosition.y = jumpHeight;
            else newPosition.y = 0;
            transform.position = newPosition;
    }

    void Crouch(){

        if (controller.Jumping())
            return;
        
        Vector3 newPosition = transform.position;
        if (controller.Crouched())
            newPosition.y = crouchHeight;
        else
            newPosition.y = 0;
        transform.position = newPosition;

    }
}
