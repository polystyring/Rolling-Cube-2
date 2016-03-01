using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{

    public float range;

    public Rigidbody rbodyToRotate, rbodyOfParent;

    void Start()
    {

    }


    void Update()
    {

        float horInput = Input.GetAxis("Horizontal");
        float horPos = horInput * range;

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -horPos);

    }
}
