using UnityEngine;
using System.Collections;

public class CheckpointTicker : MonoBehaviour
{

    public int checkpointNumber;
    public CheckpointManager cpm;

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Player")
            cpm.whichCheckpoint = checkpointNumber;
    }
}
