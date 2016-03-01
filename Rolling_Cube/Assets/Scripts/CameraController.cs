using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Rigidbody rbodyToFollow;
    public Transform transformToFollow, transformToLookAt, transformDefaultLook;
    private Vector3 cameraPositionTarget, positionVelocity = Vector3.zero, lookVelocity = Vector3.zero;
    public float smoothMove, smoothLook;
    private float inputVertical, inputHorizontal;


    void Start()
    {
        transformToLookAt.position = transformDefaultLook.position;
    }

    void FixedUpdate()
    {

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




        if (upSpeed > 0)
        {
            transformToLookAt.Translate(-Vector3.up * upSpeed * .015f);
            transformToLookAt.Translate(-Vector3.forward * upSpeed * .01f);
        }



        transform.LookAt(transformToLookAt);


    }


}