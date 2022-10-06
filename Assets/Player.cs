using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Controller controller;
    float speedPerStep = .3f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void Run(){
        if (controller.steppedThisFrame){
            Vector3 newPosition = transform.position;
            if (controller.steppedThisFrame)
                newPosition.z += speedPerStep;
            transform.position = newPosition;
        }
    }
}
