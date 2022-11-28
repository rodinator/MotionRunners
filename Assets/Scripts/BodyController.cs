using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : Controller
{

    /*Landmark Reference:
        https://techvidvan.com/tutorials/human-pose-estimation-opencv/
    */
    bool calibrated = false;
    
    Vector3 rightLeg;
    public Vector3 leftLeg;
    public Vector3 leftHip;
    Vector3 rightHip;
    Vector3 nose;
    Vector3 rightHand;
    Vector3 leftHand;
    BlazePoseSample blazePose;



    float groundHeight = .93f;
    float bodyHeight = 0;
    public float bodyHeightPercentageForStep = 5;
    bool rightLegMadeLastStep = false;

    public float bodyHeightPercentageForJump = 10;

    public float bodyHeightPercentageForCrouch = 17f;

    int steps = 0;

    bool leftLegIsReadyToStep = false;
    bool rightLegIsReadyToStep = false;

    float groundedThreeshold = 0.0003f;

    GameObject cameraView;

    // Start is called before the first frame update
    void Start()
    {    
        blazePose = FindObjectOfType<BlazePoseSample>();
        cameraView = blazePose.cameraView.gameObject;
    }

    // Update is called once per frame
    void Update()
    {


        if (AssignBodyParts())
        {
            Calibrate();
        }
        else calibrated = false;
    }

   

    bool AssignBodyParts()
    {

        
        leftLeg = blazePose.GetPoseFor(27);
        rightLeg = blazePose.GetPoseFor(28);
        leftHip = blazePose.GetPoseFor(23);
        rightHip = blazePose.GetPoseFor(24);
        leftHand = blazePose.GetPoseFor(15);
        rightHand = blazePose.GetPoseFor(16);
        nose = blazePose.GetPoseFor(0);


       
        /*/Debuging with hands and face
        leftLeg = landmarks[15];
        rightLeg = landmarks[16];
        leftHip = landmarks[7];
        rightHip = landmarks[8];*/

        return true;

    }

    public override bool Stepped()
    {

        if (!calibrated) return false;

        bool steppedThisFrame = false;
        float stepHeightFromGround = bodyHeight / 100 * bodyHeightPercentageForStep;
        /*
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
          */
        if (leftLeg.y < groundHeight + groundedThreeshold)
            leftLegIsReadyToStep = true;

        if (leftLegIsReadyToStep && leftLeg.y > groundHeight + stepHeightFromGround)
        {
            steppedThisFrame = true;
            leftLegIsReadyToStep = false;
        }

        if (rightLeg.y < groundHeight + groundedThreeshold)
            rightLegIsReadyToStep = true;

        if (rightLegIsReadyToStep && rightLeg.y > groundHeight + stepHeightFromGround)
        {
            steppedThisFrame = true;
            rightLegIsReadyToStep = false;
        }

        if (steppedThisFrame)
            steps += 1;

        return steppedThisFrame;
    }

    override public bool Jumping()
    {
        if (!calibrated)
            return false;

        bool jumping = false;
        float jumpHeightFromGround = bodyHeight / 100 * bodyHeightPercentageForJump;
        if (leftLeg.y > groundHeight + jumpHeightFromGround && rightLeg.y > groundHeight + jumpHeightFromGround)
            jumping = true;
        return jumping;
    }

    override public bool Crouching()
    {
        if (!calibrated) return false;

        bool crouching = false;
        float crouchingHeightFromGround = bodyHeight / 100 * bodyHeightPercentageForCrouch;

        if (leftHip.y < groundHeight + crouchingHeightFromGround && rightHip.y < groundHeight + crouchingHeightFromGround)
            crouching = true;

        return crouching;
    }

    public override bool Ready()
    {   if (calibrated)
            return base.Ready();
        else return false;
    }

    public bool ArmsLifted()
    {
        if (leftHand.y > nose.y && rightHand.y > nose.y)
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
                bodyHeight = groundHeight + nose.y;
                cameraView.SetActive(false);
            }
        }
    }
}
