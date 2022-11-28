using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Controller controller;
    Rigidbody rigidbody;
    float speedPerStep = .3f;
    public float jumpHeight = 27f;
    public float jumpForwardMotion = 7f;

    float crouchHeight = -.4f;

    public float startSpeed = .5f;
    float speed = 3;
    public float acceleration = 5;
    public float speedCap = .12f;
    float timeSinceLastStep = 0;
    float friction = .2f;

    public float groundedRaycastDistance = 2.01f;

    float defaultSize;

    public GameObject deadScreen;
    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller>();
        rigidbody = GetComponent<Rigidbody>();
        deadScreen = GameObject.FindWithTag("DeadScreen");
        deadScreen.SetActive(false);
        defaultSize = transform.localScale.y;
        speed = startSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if (!dead){
            Run();
            Jump();
            Crouch();
        }

    }

    void FixedUpdate()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
                dead = true;
                speed = 0;
                deadScreen.SetActive(true);

        }
    }

    void Run()
    {
        if (controller.Ready()){
            speed += Time.deltaTime * acceleration;

            Vector3 newPosition = transform.position;
            newPosition.z += speed;
            transform.position = newPosition;
        }
    }

    void Jump()
    {
        if (controller.Jumping() && Grounded()){
            rigidbody.AddForce(0, jumpHeight, jumpForwardMotion, ForceMode.Impulse);
        }

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

    bool Grounded(){
        RaycastHit hit;

        Physics.Raycast(transform.position, Vector3.down, out hit, groundedRaycastDistance);

        if (hit.collider == null)
            return false;
        else return true;
    }
}
