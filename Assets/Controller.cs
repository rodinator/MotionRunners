using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    /*Landmark Reference:
        https://techvidvan.com/tutorials/human-pose-estimation-opencv/
    */

    //!!!!!!Bewegungen Werden derzeit von den Händen gelesen!!!!!!

    Landmark[] landmarks;
    Landmark rightLeg;
    Landmark leftLeg;
    Landmark leftHip;
    Landmark rightHip;

    float groundHeight = .93f;

    float stepHeight = .13f;
    bool rightLegMadeLastStep = false;
    public bool steppedThisFrame = false;

    float jumpHeight = .5f;
    public bool jumping = false;

    float  crouchingHeight = .42f;
    public bool crouching = false;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        landmarks = GetComponent<WebSocket>().landmarks;

        if (AssignBodyParts()){
            Run();
            Jump();
            Crouch();
        }
    }

    bool AssignBodyParts(){
        if (landmarks != null && landmarks.Length < 29)
            return false;

        leftLeg = landmarks[27];
        rightLeg = landmarks[28];
        leftHip = landmarks[23];
        rightHip = landmarks[24];

        //Debuging with hands and face
        leftLeg = landmarks[15];
        rightLeg = landmarks[16];
        leftHip = landmarks[7];
        rightHip = landmarks[8];

        return true;

    }

    void Run()
    {

        steppedThisFrame = false;
        float stepHeightFromGround = groundHeight - stepHeight;

            if (rightLegMadeLastStep)
            {
                if (leftLeg.y < stepHeightFromGround && rightLeg.y > stepHeightFromGround)
                {
                    rightLegMadeLastStep = false;
                    steppedThisFrame = true;
                }
            }
            else
            if (rightLeg.y < stepHeightFromGround && leftLeg.y > stepHeightFromGround)
            {
                rightLegMadeLastStep = true;
                steppedThisFrame = true;
            }
        
    }

    void Jump(){
        jumping = false;
        float jumpHeightFromGround = groundHeight - jumpHeight;
        if (leftLeg.y < jumpHeightFromGround && rightLeg.y < jumpHeightFromGround)
            jumping = true;
    }

    void Crouch(){
        crouching = false;
        float crouchingHeightFromGround = groundHeight - crouchingHeight;
        if (leftHip.y > crouchingHeightFromGround && rightHip.y > crouchingHeightFromGround)
            crouching = true;
    }
}
