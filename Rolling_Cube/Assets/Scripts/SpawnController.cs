using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour
{

    public static Transform spawnPoint1;
    public static Transform spawnPoint2;
    public static Transform spawnPoint3;
    public static Transform spawnPoint4;
    public static Transform spawnPoint5;

    public static CheckpointManager cpm;

    void Start()
    {
        spawnPoint1 = GameObject.Find("SpawnPoint1").transform;
        spawnPoint2 = GameObject.Find("SpawnPoint2").transform;
        spawnPoint3 = GameObject.Find("SpawnPoint3").transform;
        spawnPoint4 = GameObject.Find("SpawnPoint4").transform;
        spawnPoint5 = GameObject.Find("SpawnPoint5").transform;

        cpm = GameObject.Find("SceneController").GetComponent<CheckpointManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerStay(Collider c)
    {
        switch (cpm.whichCheckpoint)
        {
            case 1:               
                c.transform.position = spawnPoint1.position;
                c.transform.rotation = spawnPoint1.rotation;
                break;
            case 2:
                c.transform.position = spawnPoint2.position;
                c.transform.rotation = spawnPoint2.rotation;
                break;
            case 3:
                c.transform.position = spawnPoint3.position;
                c.transform.rotation = spawnPoint3.rotation;
                break;
            case 4:
                c.transform.position = spawnPoint4.position;
                c.transform.rotation = spawnPoint4.rotation;
                break;
            case 5:
                c.transform.position = spawnPoint5.position;
                c.transform.rotation = spawnPoint5.rotation;
                break;
        }

    }


}
