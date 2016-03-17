using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rbodyToMove;

    public float speed, rotationSpeed;
    private float defaultSpeed, defaultRotationSpeed, fallingSpeed, fallTimer;
    public float jumpPowerUp, jumpPowerFoward, jumpPowerBack, jumpPowerLeftRight;
    public float wallJumpPowerUp, wallJumpPowerFoward, wallJumpPowerBack, wallJumpPowerLeftRight;
    public float jumpMoveSpeed, jumpMoveTurn;
    public bool isGrounded, isTouchingFront, isTouchingLeft, isTouchingRight, isTouchingWall, isJumping, isFalling;
    public bool canJump, canTwist, canSlide;

    RaycastHit[] belowBlackBoxList = new RaycastHit[4];
    RaycastHit[] frontBlackBoxList = new RaycastHit[4];
    RaycastHit[] leftBlackBoxList = new RaycastHit[4];
    RaycastHit[] rightBlackBoxList = new RaycastHit[4];

    float inputVertical, inputHorizontal;

    public bool isTwisting = false;
    bool twistingLeft = false, twistingRight = false;
    public float twistSpeed;
    Quaternion twistTarget;
    float twistAngle;

    public bool isSliding = false;
    public float slideSpeed;
    float slideTime;

    float timeCheck;


    void Start()
    {
        defaultSpeed = speed;
        defaultRotationSpeed = rotationSpeed;

        //canJump = false;
        //canTwist = false;
        //canSlide = false;
    }



    void Update()
    {
        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");
        

        isGrounded = false;

        Ray groundCheckNW = new Ray(transform.TransformPoint(-.5f, 0, .5f), -transform.up);
        Physics.Raycast(groundCheckNW, out belowBlackBoxList[0], .6f);

        Ray groundCheckNE = new Ray(transform.TransformPoint(.5f, 0, .5f), -transform.up);
        Physics.Raycast(groundCheckNE, out belowBlackBoxList[1], .6f);

        Ray groundCheckSW = new Ray(transform.TransformPoint(-.5f, 0, -.5f), -transform.up);
        Physics.Raycast(groundCheckSW, out belowBlackBoxList[2], .6f);

        Ray groundCheckSE = new Ray(transform.TransformPoint(.5f, 0, -.5f), -transform.up);
        Physics.Raycast(groundCheckSE, out belowBlackBoxList[3], .6f);

        for (int i = 0; i < belowBlackBoxList.Length && !isGrounded; i++)
        {
            if (belowBlackBoxList[i].collider != null && belowBlackBoxList[i].collider.gameObject.layer != 8)
            {
                isGrounded = true;
                isFalling = false;
                fallingSpeed = 0;
                fallTimer = 0;
            }
        }


        isTouchingFront = false;

        Ray frontCheckNW = new Ray(transform.TransformPoint(-.5f, .5f, 0), transform.forward);
        Physics.Raycast(frontCheckNW, out frontBlackBoxList[0], .65f);

        Ray frontCheckNE = new Ray(transform.TransformPoint(.5f, .5f, 0), transform.forward);
        Physics.Raycast(frontCheckNE, out frontBlackBoxList[1], .65f);

        Ray frontCheckSW = new Ray(transform.TransformPoint(-.5f, -.45f, 0), transform.forward);
        Physics.Raycast(frontCheckSW, out frontBlackBoxList[2], .65f);

        Ray frontCheckSE = new Ray(transform.TransformPoint(.5f, -.45f, 0), transform.forward);
        Physics.Raycast(frontCheckSE, out frontBlackBoxList[3], .65f);

        for (int i = 0; i < frontBlackBoxList.Length; i++)
        {
            if (frontBlackBoxList[i].collider != null && frontBlackBoxList[i].collider.gameObject.layer != 8)
            {
                isTouchingFront = true;
            }
        }


        isTouchingLeft = false;

        Ray leftCheckNW = new Ray(transform.TransformPoint(0, .5f, -.5f), -transform.right);
        Physics.Raycast(leftCheckNW, out leftBlackBoxList[0], .65f);

        Ray leftCheckNE = new Ray(transform.TransformPoint(0, .5f, .5f), -transform.right);
        Physics.Raycast(frontCheckNE, out leftBlackBoxList[1], .65f);

        Ray leftCheckSW = new Ray(transform.TransformPoint(0, -.45f, -.5f), -transform.right);
        Physics.Raycast(leftCheckSW, out leftBlackBoxList[2], .65f);

        Ray leftCheckSE = new Ray(transform.TransformPoint(0, -.45f, .5f), -transform.right);
        Physics.Raycast(leftCheckSE, out leftBlackBoxList[3], .65f);

        for (int i = 0; i < leftBlackBoxList.Length; i++)
        {
            if (leftBlackBoxList[i].collider != null && leftBlackBoxList[i].collider.gameObject.layer != 8)
            {                
                isTouchingLeft = true;
            }

        }


        isTouchingRight = false;

        Ray rightCheckNW = new Ray(transform.TransformPoint(0, .5f, .5f), transform.right);
        Physics.Raycast(rightCheckNW, out rightBlackBoxList[0], .65f);

        Ray rightCheckNE = new Ray(transform.TransformPoint(0, .5f, -.5f), transform.right);
        Physics.Raycast(rightCheckNE, out rightBlackBoxList[1], .65f);

        Ray rightCheckSW = new Ray(transform.TransformPoint(0, -.45f, .5f), transform.right);
        Physics.Raycast(rightCheckSW, out rightBlackBoxList[2], .65f);

        Ray rightCheckSE = new Ray(transform.TransformPoint(0, -.45f, -.5f), transform.right);
        Physics.Raycast(rightCheckSE, out rightBlackBoxList[3], .65f);

        for (int i = 0; i < rightBlackBoxList.Length; i++)
        {
            if (rightBlackBoxList[i].collider != null && rightBlackBoxList[i].collider.gameObject.layer != 8)
            {                
                isTouchingRight = true;
            }

        }

        if (isTouchingFront || isTouchingLeft || isTouchingRight)
            isTouchingWall = true;
        else
            isTouchingWall = false;


        //Debug.DrawRay(transform.TransformPoint(0, .5f, .5f), transform.right, Color.red, 2);
        //Debug.DrawRay(transform.TransformPoint(0, .5f, -.5f), transform.right, Color.blue, 2);
        //Debug.DrawRay(transform.TransformPoint(0, -.45f, .5f), transform.right, Color.green, 2);
        //Debug.DrawRay(transform.TransformPoint(0, -.45f, -.5f), transform.right, Color.yellow, 2);


        isJumping = false;

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded && canJump)
            {
                isJumping = true;

                rbodyToMove.AddForce(transform.up * jumpPowerUp, ForceMode.Impulse);

                if (inputVertical > 0)
                    rbodyToMove.AddForce(transform.forward * inputVertical * jumpPowerFoward, ForceMode.Impulse);
                if (inputVertical < 0)
                    rbodyToMove.AddForce(transform.forward * inputVertical * jumpPowerBack, ForceMode.Impulse);
                if (inputHorizontal != 0)
                    rbodyToMove.AddForce(transform.right * inputHorizontal * jumpPowerLeftRight, ForceMode.Impulse);
            }

            if (!isGrounded && isTouchingWall)
            {
                isJumping = true;

                rbodyToMove.velocity = Vector3.zero;

                rbodyToMove.AddForce(transform.up * wallJumpPowerUp, ForceMode.Impulse);

                if (inputVertical > 0)
                    rbodyToMove.AddForce(transform.forward * inputVertical * wallJumpPowerFoward, ForceMode.Impulse);
                if (inputVertical < 0)
                    rbodyToMove.AddForce(transform.forward * inputVertical * wallJumpPowerBack, ForceMode.Impulse);
                if (inputHorizontal != 0)
                    rbodyToMove.AddForce(transform.right * inputHorizontal * wallJumpPowerLeftRight, ForceMode.Impulse);
            }


        }

        //print(rbodyToMove.velocity.magnitude);


        if (Input.GetButtonDown("Left Bumper") && canTwist && !isTwisting && !isSliding)
        {
            rbodyToMove.angularVelocity = Vector3.zero;
            twistTarget = Quaternion.Euler(transform.eulerAngles) * Quaternion.Euler(0, -90, 0);
            isTwisting = true;
            twistingLeft = true;
        }

        if (Input.GetButtonDown("Right Bumper") && canTwist && !isTwisting && !isSliding)
        {
            rbodyToMove.angularVelocity = Vector3.zero;
            twistTarget = Quaternion.Euler(transform.eulerAngles) * Quaternion.Euler(0, 90, 0);
            isTwisting = true;
            twistingRight = true;
        }

        if (isTwisting)
        {
            rotationSpeed = 0;
            twistAngle = Quaternion.Angle(transform.rotation, twistTarget);

            //print(twistAngle);

            if (twistAngle <= 5)
            {
                rbodyToMove.transform.rotation = twistTarget;
                rbodyToMove.angularVelocity = Vector3.zero;
                rotationSpeed = defaultRotationSpeed;
                isTwisting = false;
                twistingRight = false;
                twistingRight = false;
            }
            else
            {
                if (twistingRight)
                    rbodyToMove.AddTorque(transform.up * twistSpeed * Time.deltaTime, ForceMode.Impulse);
                else
                    rbodyToMove.AddTorque(transform.up * -twistSpeed * Time.deltaTime, ForceMode.Impulse);
            }
        }



        if (Input.GetButtonDown("Slide Left") && canSlide && !isSliding && !isTwisting)
        {
            rbodyToMove.AddForce(-transform.right * slideSpeed, ForceMode.Impulse);
            rbodyToMove.angularVelocity = Vector3.zero;
            isSliding = true;
            slideTime = .25f;
        }

        if (Input.GetButtonDown("Slide Right") && canSlide && !isSliding && !isTwisting)
        {
            rbodyToMove.AddForce(transform.right * slideSpeed, ForceMode.Impulse);
            rbodyToMove.angularVelocity = Vector3.zero;
            isSliding = true;
            slideTime = .25f;
        }

        if (isSliding == true)
        {
            slideTime -= Time.deltaTime;
            if (slideTime <= 0)
                isSliding = false;
        }

    }


    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical") * speed;
        float h = Input.GetAxis("Horizontal") * rotationSpeed;

        if (v != 0 || h != 0)
        {
            if (isGrounded)
            {
                rbodyToMove.AddForce(rbodyToMove.transform.forward * v);
                rbodyToMove.AddTorque(rbodyToMove.transform.up * h);
            }
            else
            {
                rbodyToMove.AddForce(rbodyToMove.transform.forward * v * jumpMoveSpeed);
                rbodyToMove.AddTorque(rbodyToMove.transform.up * h * jumpMoveTurn);
            }

        }


        float downSpeed = Vector3.Dot(rbodyToMove.velocity, -rbodyToMove.transform.up);


        if (!isGrounded && downSpeed > 0)
        {
            fallTimer += 1;

            if (fallTimer > 3.5f)
            {
                //print(downSpeed);
                isFalling = true;
                fallingSpeed += 2f;
                rbodyToMove.AddForce(-rbodyToMove.transform.up * fallingSpeed, ForceMode.Acceleration);
            }
        }




    }
}