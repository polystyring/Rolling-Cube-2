using UnityEngine;
using System.Collections;

public class canJumpPowerup : MonoBehaviour
{

    public float jumpSpeed;
    float timer;
    public Rigidbody rbodyToJump;
    public AudioSource jumpSound;

    void Start()
    {
        timer = jumpSpeed;
    }

    void Update()
    {
        timer -= 1 * Time.deltaTime;
        if (timer <= 0)
        {
            jumpSound.Play();
            rbodyToJump.AddForce(transform.up * 5, ForceMode.Impulse);
            timer = jumpSpeed;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag == "Player")
        {
            PlayerController pc = c.gameObject.GetComponent<PlayerController>();
            pc.canJump = true;

            c.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 33, ForceMode.Impulse);

            Roller r = GameObject.Find("PlayerCubeChild").GetComponent<Roller>();
            r.jumpSound.Play();

            Destroy(gameObject);

        }
    }


}
