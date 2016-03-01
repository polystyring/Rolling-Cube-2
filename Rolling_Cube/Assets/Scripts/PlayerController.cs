using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rbodyToMove;

    public float speed, rotationSpeed;
    public float jumpPowerUp, jumpPowerFoward, jumpPowerBack, jumpPowerLeftRight;
    public float jumpMoveSpeed, jumpMoveTurn;
    public bool isGrounded;

    public RaycastHit[] blackBoxList = new RaycastHit[4];


    void Update()
    {
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
            if (blackBoxList[i].collider != null)
                isGrounded = true;
        }

        foreach (RaycastHit r in blackBoxList)
            print(r.collider);

        print(isGrounded);

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rbodyToMove.AddForce(transform.up * jumpPowerUp, ForceMode.Impulse);

            if (Input.GetAxis("Vertical") > 0)
                rbodyToMove.AddForce(transform.forward * jumpPowerFoward, ForceMode.Impulse);
            if (Input.GetAxis("Vertical") < 0)
                rbodyToMove.AddForce(-transform.forward * jumpPowerBack, ForceMode.Impulse);
            if (Input.GetAxis("Horizontal") > 0)
                rbodyToMove.AddForce(transform.right * jumpPowerLeftRight, ForceMode.Impulse);
            if (Input.GetAxis("Horizontal") < 0)
                rbodyToMove.AddForce(-transform.right * jumpPowerLeftRight, ForceMode.Impulse);

        }
    }



    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical") * speed;
        float h = Input.GetAxis("Horizontal") * rotationSpeed;

        if (v != 0 || h != 0)
        {
            if (isGrounded == true)
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
    }
}