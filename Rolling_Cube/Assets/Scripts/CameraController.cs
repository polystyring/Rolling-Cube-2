using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Camera cameraMain;
    public Rigidbody rbodyToFollow;
    public Transform playerCubeTransform, transformToFollow, transformToLookAt, transformDefaultLook;
    private Vector3 cameraPositionTarget, positionVelocity = Vector3.zero, lookVelocity = Vector3.zero;
    private Vector3 screenPoint;
    public float smoothMove, smoothLook;
    private float defaultSmoothMove, defaultSmoothLook;
    private float inputVertical, inputHorizontal;
    bool isGrounded, isTwisting, isSliding;

    PlayerController playerController;

    void Start()
    {
        transformToLookAt.position = transformDefaultLook.position;
        playerController = rbodyToFollow.GetComponent<PlayerController>();

        defaultSmoothMove = smoothMove;
        defaultSmoothLook = smoothLook;
    }

    void FixedUpdate()
    {
        isGrounded = playerController.isGrounded;

        isTwisting = playerController.isTwisting;
        isSliding = playerController.isSliding;
        if (isTwisting)
            smoothMove = .05f;
        else if (isSliding)
            smoothMove = .1f;
        else
            smoothMove = defaultSmoothMove;



        //print(smoothMove);


        //screenPoint = cameraMain.WorldToViewportPoint(playerCubeTransform.position);
        //if (screenPoint.x > .8f)
        //    print("right");
        //if (screenPoint.x < .2f)
        //    print("left");
        //if (screenPoint.y > .8f)
        //    print("top");
        //if (screenPoint.y < .2f)
        //    print("bottom");



        cameraPositionTarget = transformToFollow.position;
        transform.position = Vector3.SmoothDamp(transform.position, cameraPositionTarget, ref positionVelocity, smoothMove);

        inputVertical = Input.GetAxis("CameraVertical");
        inputHorizontal = Input.GetAxis("CameraHorizontal");

        var upSpeed = Vector3.Dot(rbodyToFollow.velocity, rbodyToFollow.transform.up);
        if (upSpeed < .5f)
            upSpeed = 0;

        //print(upSpeed);

        if (inputVertical > 0)
            transformToLookAt.Translate(transformToLookAt.up * inputVertical * smoothLook);
        if (inputVertical < 0)
            transformToLookAt.Translate(transformToLookAt.up * inputVertical * smoothLook);
        if (inputHorizontal > 0)
            transformToLookAt.Translate(Vector3.right * inputHorizontal * smoothLook);
        if (inputHorizontal < 0)
            transformToLookAt.Translate(Vector3.right * inputHorizontal * smoothLook);
        if (inputVertical == 0 && inputHorizontal == 0 && upSpeed == 0)
            transformToLookAt.position = Vector3.SmoothDamp(transformToLookAt.position, transformDefaultLook.position, ref lookVelocity, smoothMove);


        //print(Input.GetAxis("CameraHorizontal"));
        //print(Input.GetAxis("CameraVertical"));
        //print(Input.GetAxis("Horizontal"));
        //print(Input.GetAxis("Vertical"));


        //if (upSpeed > 0 && !isGrounded)
        //{
        //    transformToLookAt.Translate(-Vector3.up * upSpeed * .015f);
        //    transformToLookAt.Translate(-Vector3.forward * upSpeed * .01f);
        //}



        transform.LookAt(transformToLookAt);


    }


}