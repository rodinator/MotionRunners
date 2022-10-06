using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    /*Landmark Reference:
        https://techvidvan.com/tutorials/human-pose-estimation-opencv/
    */

    Landmark[] landmarks;
    float groundHeight = .93f;

    float stepHeight = .8f;
    bool rightLegMadeLastStep = false;
    public bool steppedThisFrame = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        landmarks = GetComponent<WebSocket>().landmarks;

        Run();
    }

    void Run()
    {

        steppedThisFrame = false;

        if (landmarks != null && landmarks.Length >= 29){

            Landmark leftLeg = landmarks[27];
            Landmark rightLeg = landmarks[28];

            leftLeg = landmarks[15];
            rightLeg = landmarks[16];

            if (rightLegMadeLastStep)
            {
                if (leftLeg.y < stepHeight && rightLeg.y > stepHeight)
                {
                    rightLegMadeLastStep = false;
                    steppedThisFrame = true;
                    print("step left");
                }
            }
            else
            if (rightLeg.y < stepHeight && leftLeg.y > stepHeight)
            {
                rightLegMadeLastStep = true;
                steppedThisFrame = true;
                print("step right");
            }

        }
        
    }
}
