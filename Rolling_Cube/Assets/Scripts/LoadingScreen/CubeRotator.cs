using UnityEngine;
using System.Collections;

public class CubeRotator : MonoBehaviour
{

    public Renderer cubeRenderer;
    public float appearSpeed;
    public bool isVisible = false, isStarting = false;

    void Start()
    {
        appearSpeed = 1;
        cubeRenderer.material.SetFloat("_Cutoff", 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up * 5 * Time.deltaTime);
        transform.Rotate(transform.right * 5 * Time.deltaTime);
        transform.Rotate(transform.forward * 5 * Time.deltaTime);


        if (appearSpeed >= .05f && !isStarting)
        {
            Appear();
        }
        else
            isVisible = true;

        if (isVisible && Input.GetButtonDown("Start"))
        {
            isStarting = true;
        }


        if (isStarting)           
            Disappear();
    }

    public void Appear()
    {
        appearSpeed -= .15f * Time.deltaTime;
        cubeRenderer.material.SetFloat("_Cutoff", appearSpeed);
    }

    public void Disappear()
    {
        appearSpeed += .15f * Time.deltaTime;
        cubeRenderer.material.SetFloat("_Cutoff", appearSpeed);

        if (appearSpeed >= 1)
            LoadingScreenController.async.allowSceneActivation = true;
    }

}
