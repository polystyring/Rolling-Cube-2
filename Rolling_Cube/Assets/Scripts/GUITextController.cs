using UnityEngine;
using System.Collections;

public class GUITextController : MonoBehaviour
{

    public GameObject LStickText, AButtonText, SlideButtonText;
    public PlayerController cubeController;
    float timer;
    bool jumpCountdownStarted = false, slideCountdownStarted = false;

    void Start()
    {
        LStickText.SetActive(false);
        AButtonText.SetActive(false);
        SlideButtonText.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (Time.timeSinceLevelLoad >= 2 && Time.timeSinceLevelLoad < 10)
            LStickText.SetActive(true);

        if (Time.timeSinceLevelLoad >= 10)
            LStickText.SetActive(false);


        if (cubeController.canJump && !jumpCountdownStarted)
        {
            AButtonText.SetActive(true);
            timer = Time.time;
            jumpCountdownStarted = true;
        }

        if (cubeController.canJump && jumpCountdownStarted && Time.time - timer > 8 )
        {
            AButtonText.SetActive(false);            
        }


        if (cubeController.canSlide && !slideCountdownStarted)
        {
            SlideButtonText.SetActive(true);
            timer = Time.time;
            slideCountdownStarted = true;
        }

        if (cubeController.canSlide && jumpCountdownStarted && Time.time - timer > 8 )
        {
            SlideButtonText.SetActive(false);            
        }


       


    }
}
