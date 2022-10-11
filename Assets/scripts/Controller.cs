using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    /*Landmark Reference:
        https://techvidvan.com/tutorials/human-pose-estimation-opencv/
    */
    bool calibrated;
    Landmark[] landmarks;
    Landmark rightLeg;
    Landmark leftLeg;
    Landmark leftHip;
    Landmark rightHip;
    Landmark nose;
    Landmark rightHand;
    Landmark leftHand;

    public float groundHeight = .93f;
    public float bodyHeight = 0;
    public float bodyHeightPercentageForStep = 5;
    public float stepHeight = .13f;
    bool rightLegMadeLastStep = false;
    public bool steppedThisFrame = false;

    public float bodyHeightPercentageForJump = 10;
    public float jumpHeight = .2f;
    public bool jumping = false;

    public float bodyHeightPercentageForCrouch = 27.5f;
    public float crouchingHeight = .42f;
    public bool crouching = false;

    int steps = 0;


    public float debugLeftLegY;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        landmarks = GetComponent<WebSocket>().landmarks;


        if (AssignBodyParts())
        {
            Calibrate();
            if (calibrated)
            {

                debugLeftLegY = leftLeg.y;
                Run();
                Jump();
                Crouch();
            }
        }
    }

    bool AssignBodyParts()
    {
        if (landmarks != null && landmarks.Length < 29)
            return false;

        leftLeg = landmarks[27];
        rightLeg = landmarks[28];
        leftHip = landmarks[23];
        rightHip = landmarks[24];
        leftHand = landmarks[15];
        rightHand = landmarks[16];
        nose = landmarks[0];

        /*/Debuging with hands and face
        leftLeg = landmarks[15];
        rightLeg = landmarks[16];
        leftHip = landmarks[7];
        rightHip = landmarks[8];*/

        return true;

    }

    void Run()
    {

        steppedThisFrame = false;
        float stepHeightFromGround = bodyHeight / 100 * bodyHeightPercentageForStep;

        if (rightLegMadeLastStep)
        {
            if (leftLeg.y < groundHeight - stepHeightFromGround && rightLeg.y > groundHeight - stepHeightFromGround)
            {
                rightLegMadeLastStep = false;
                steppedThisFrame = true;
            }
        }
        else
        if (rightLeg.y < groundHeight - stepHeightFromGround && leftLeg.y >  groundHeight - stepHeightFromGround)
        {
            rightLegMadeLastStep = true;
            steppedThisFrame = true;
        }

        if (steppedThisFrame)
            steps += 1;

    }

    void Jump()
    {
        jumping = false;
        float jumpHeightFromGround = bodyHeight / 100 * bodyHeightPercentageForJump;
        if (leftLeg.y < groundHeight - jumpHeightFromGround && rightLeg.y < groundHeight - jumpHeightFromGround)
            jumping = true;
    }

    void Crouch()
    {
        crouching = false;
        float crouchingHeightFromGround = bodyHeight / 100 * bodyHeightPercentageForCrouch;

        if (leftHip.y > groundHeight - crouchingHeightFromGround && rightHip.y > groundHeight - crouchingHeightFromGround)
            crouching = true;
    }

    public bool ArmsLifted()
    {
        if (leftHand.y < nose.y && rightHand.y < nose.y)
            return true;
        else return false;
    }

    void Calibrate()
    {
        if (calibrated == false)
        {
            print("Lift Arms to Calibrate");
            if (ArmsLifted())
            {
                calibrated = true;
                groundHeight = (leftLeg.y + rightLeg.y) / 2;
                bodyHeight = groundHeight - nose.y;

            }
        }
    }
}