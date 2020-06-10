using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public GameObject actualDoor;
    public Transform CloseLoc;

    private bool startMoving;

    private void Update()
    {
        if(startMoving)
        actualDoor.transform.position = Vector3.MoveTowards(actualDoor.transform.position, CloseLoc.position, 0.2f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            startMoving = true;
        }
    }
}
