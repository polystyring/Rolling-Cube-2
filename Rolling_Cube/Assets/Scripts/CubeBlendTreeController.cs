using UnityEngine;
using System.Collections;

public class CubeBlendTreeController : MonoBehaviour
{

    public Rigidbody rbodyOfParent;
    public Animator cubeAnimator;
    float cubeVelocity, cubeTorque;
    float m_GroundCheckDistance = 0.1f;
    Vector3 m_GroundNormal;



    void Start()
    {

    }

    void Update()
    {
        Vector3 move = transform.position;

        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            m_GroundNormal = hitInfo.normal;
            //m_IsGrounded = true;
            //m_Animator.applyRootMotion = true;
        }
        else
        {
            //m_IsGrounded = false;
            m_GroundNormal = Vector3.up;
            //m_Animator.applyRootMotion = false;

        }



        if (move.magnitude > 1f)
            move.Normalize();

        move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        float m_TurnAmount = Mathf.Atan2(move.x, move.z);
        float m_ForwardAmount = move.z;


        //cubeVelocity = Vector3.Dot(rbodyOfParent.velocity, rbodyOfParent.transform.forward);
        cubeAnimator.SetFloat("cubeVelocity", m_ForwardAmount);

        //cubeTorque = rbodyOfParent.angularVelocity.magnitude;
        cubeAnimator.SetFloat("cubeTorque", m_TurnAmount);


    }


}
