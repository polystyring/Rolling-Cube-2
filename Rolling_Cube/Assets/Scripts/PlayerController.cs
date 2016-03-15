using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rbodyToMove;

    public float speed, rotationSpeed;
    private float defaultSpeed, defaultRotationSped, fallingSpeed, fallTimer;
    public float jumpPowerUp, jumpPowerFoward, jumpPowerBack, jumpPowerLeftRight;
    public float jumpMoveSpeed, jumpMoveTurn;
    public bool isGrounded, isJumping;
    public bool canJump, canTwist, canSlide;   

    public RaycastHit[] blackBoxList = new RaycastHit[4];

    float inputVertical, inputHorizontal;

    float scaledSpeed, inverseSpeed;
    public bool isTwisting = false;
    float twistTime;
    public float twistSpeed;

    public float slideSpeed;
    public bool isSliding = false;
    float slideTime;



    void Start()
    {
        defaultSpeed = speed;
        defaultRotationSped = rotationSpeed;

        //canJump = false;
        //canTwist = false;
        //canSlide = false;
    }



    void Update()
    {
        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");

        //print(inputVertical + "Vertical");
        //print(inputHorizontal + "Horizontal");

        //        var upSpeed = Vector3.Dot(rbodyToMove.velocity, rbodyToMove.transform.up);
        //        var downSpeed = Vector3.Dot(rbodyToMove.velocity, -rbodyToMove.transform.up);
        //        var rightSpeed = Vector3.Dot(rbodyToMove.velocity, rbodyToMove.transform.right);
        //        var leftSpeed = Vector3.Dot(rbodyToMove.velocity, -rbodyToMove.transform.right);
        //        var forwardSpeed = Vector3.Dot(rbodyToMove.velocity, rbodyToMove.transform.forward);
        //        var backwardSpeed = Vector3.Dot(rbodyToMove.velocity, -rbodyToMove.transform.forward);

        //print(leftSpeed);

        isGrounded = false;

        Ray groundCheckNW = new Ray(transform.TransformPoint(-.5f, 0, .5f), -transform.up);
        Physics.Raycast(groundCheckNW, out blackBoxList[0], .6f);

        Ray groundCheckNE = new Ray(transform.TransformPoint(.5f, 0, .5f), -transform.up);
        Physics.Raycast(groundCheckNE, out blackBoxList[1], .6f);

        Ray groundCheckSW = new Ray(transform.TransformPoint(-.5f, 0, -.5f), -transform.up);
        Physics.Raycast(groundCheckSW, out blackBoxList[2], .6f);

        Ray groundCheckSE = new Ray(transform.TransformPoint(.5f, 0, -.5f), -transform.up);
        Physics.Raycast(groundCheckSE, out blackBoxList[3], .6f);

        for (int i = 0; i < blackBoxList.Length && isGrounded == false; i++)
        {
            if (blackBoxList[i].collider != null && blackBoxList[i].collider.gameObject.layer != 8)
            {
                isGrounded = true;
                fallingSpeed = 0;
                fallTimer = 0;
            }
        }

        //foreach (RaycastHit r in blackBoxList)
        //    print(r.collider);

        //print(isGrounded);

        isJumping = false;

        if (Input.GetButtonDown("Jump") && isGrounded && canJump)
        {
            isJumping = true;

            rbodyToMove.AddForce(transform.up * jumpPowerUp, ForceMode.Impulse);

            if (inputVertical > 0)
                rbodyToMove.AddForce(transform.forward * inputVertical * jumpPowerFoward, ForceMode.Impulse);
            if (inputVertical < 0)
                rbodyToMove.AddForce(transform.forward * inputVertical * jumpPowerBack, ForceMode.Impulse);
            if (inputHorizontal > 0)
                rbodyToMove.AddForce(transform.right * inputHorizontal * jumpPowerLeftRight, ForceMode.Impulse);
            if (inputHorizontal < 0)
                rbodyToMove.AddForce(transform.right * inputHorizontal * jumpPowerLeftRight, ForceMode.Impulse);
        }

        //print(rbodyToMove.velocity.magnitude);


        if (Input.GetButtonDown("Left Bumper") && canTwist && !isTwisting && !isSliding)
        {
            scaledSpeed = rbodyToMove.velocity.magnitude / twistSpeed;
            inverseSpeed = 1 - scaledSpeed;

            rbodyToMove.velocity = Vector3.zero;
            rbodyToMove.angularVelocity = Vector3.zero;
            rbodyToMove.AddTorque(transform.up * -15, ForceMode.Impulse);

            isTwisting = true;
            twistTime = .5f;
            rotationSpeed = 0;
            speed = 0;
        }

        if (Input.GetButtonDown("Right Bumper") && canTwist && !isTwisting && !isSliding)
        {
            scaledSpeed = rbodyToMove.velocity.magnitude / twistSpeed;
            inverseSpeed = 1 - scaledSpeed;

            rbodyToMove.velocity = Vector3.zero;
            rbodyToMove.angularVelocity = Vector3.zero;
            rbodyToMove.AddTorque(transform.up * 15 * inverseSpeed, ForceMode.Impulse);

            isTwisting = true;
            twistTime = .5f;
            rotationSpeed = 0;
            speed = 0;
        }

        if (isTwisting == true)
        {
            twistTime -= Time.deltaTime;
            if (twistTime <= 0)
            {
                isTwisting = false;
                rotationSpeed = defaultRotationSped;
                speed = defaultSpeed;
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
                fallingSpeed += 2.5f;
                rbodyToMove.AddForce(-rbodyToMove.transform.up * fallingSpeed, ForceMode.Acceleration);
            }
        }
            



    }
}