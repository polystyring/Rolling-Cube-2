using UnityEngine;
using System.Collections;

public class canSlidePowerup : MonoBehaviour
{

    public float slideSpeed;
    float timer;
    public Rigidbody rbodyToSlide;
    //public AudioSource jumpSound;
    bool slidingRight = true;

    void Start()
    {
        timer = slideSpeed;
    }

    void Update()
    {
        timer -= 1 * Time.deltaTime;
        if (timer <= 0)
        {
            //jumpSound.Play();
            if (slidingRight)
                rbodyToSlide.AddForce(transform.right * 10, ForceMode.Impulse);
            else
                rbodyToSlide.AddForce(-transform.right * 10, ForceMode.Impulse);

            timer = slideSpeed;
            slidingRight = !slidingRight;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag == "Player")
        {
            PlayerController pc = c.gameObject.GetComponent<PlayerController>();
            pc.canSlide = true;
            
            c.gameObject.GetComponent<Rigidbody>().AddForce(transform.right * pc.slideSpeed, ForceMode.Impulse);

            //Roller r = GameObject.Find("PlayerCubeChild").GetComponent<Roller>();
            //r.jumpSound.Play();

            Destroy(gameObject);

        }
    }


}