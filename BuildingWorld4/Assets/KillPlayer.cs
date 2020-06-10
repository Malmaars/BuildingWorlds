using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public Transform spawnLoc;
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            respawnPlayer(other.transform);
        }
    }

    public void respawnPlayer(Transform other)
    {
        other.GetComponent<PlayerMovement>().resetAmmo();
        other.GetComponent<PlayerMovement>().respawn = 0;
        other.position = spawnLoc.position;
    }
}
