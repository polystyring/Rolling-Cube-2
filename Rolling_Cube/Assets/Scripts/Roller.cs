using UnityEngine;
using System.Collections;

public class Roller : MonoBehaviour
{

    public float rotationSpeed, rotationMultiplier, maxRotationSpeed;
    public Rigidbody rbodyToRotate, rbodyOfParent;
    private Vector3 forwardSpeed;
    

    void Start()
    {

    }

    void Update()
    {
        rotationSpeed = rbodyOfParent.velocity.magnitude * rotationMultiplier;
        if (rotationSpeed > maxRotationSpeed)
            rotationSpeed = maxRotationSpeed;
               

        var forwardSpeed = Vector3.Dot(rbodyOfParent.velocity, rbodyOfParent.transform.forward);

        //print(forwardSpeed);

        if (forwardSpeed > 0)
            rbodyToRotate.transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
        else if (forwardSpeed < 0)
            rbodyToRotate.transform.Rotate(-rotationSpeed * Time.deltaTime, 0, 0);

                

    }
}
