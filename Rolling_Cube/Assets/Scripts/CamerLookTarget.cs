using UnityEngine;
using System.Collections;

public class CamerLookTarget : MonoBehaviour
{
    Vector3 follow;
     

    void Start()
    {

        
    }


    void Update()
    {
        follow = GameObject.Find("Main Camera").GetComponent<CameraController>().transformToLookAt.position;
        transform.position = follow;



    }
}
