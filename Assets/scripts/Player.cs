using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Controller controller;
    Rigidbody rigidbody;
    float speedPerStep = .3f;
    public float jumpHeight = 10f;
    float crouchHeight = -.4f;

    float speed = 0;
    float speedCap = .12f;
    float speedMod = 0.08f;
    float timeSinceLastStep = 0;
    float friction = .2f;

    float defaultSize;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller>();
        rigidbody = GetComponent<Rigidbody>();
        defaultSize = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        Crouch();


    }

    void FixedUpdate()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            speed = 0;
            Vector3 newPosition = transform.position;
            newPosition.z -= 5;
            transform.position = newPosition;
        }
    }

    void Run()
    {

        timeSinceLastStep += Time.deltaTime;
        speed -= friction * speedMod;

        if (controller.Stepped())
        {
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

        print(finalSpeed);
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

    float currentJumpSpeed = .3f;
    float maxJumpSpeed = .5f;
    float jumpAcceleration = .2f;

    void Jump()
    {
        Vector3 newPosition = transform.position;
        if (controller.Jumping())
            rigidbody.AddForce(0, jumpHeight, 0, ForceMode.Impulse);

        //else newPosition.y = 0;
        //transform.position = newPosition;
    }

    void Crouch()
    {

        Vector3 newSize = transform.localScale;
        if (controller.Crouching())
            newSize.y = defaultSize /2;
        else
            newSize.y = defaultSize;
        transform.localScale = newSize;

    }

}
