using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    //The target on the ground
    public GameObject target;

    //The place where the feet hit the ground
    public GameObject feetEnd;

    //How fast the leg moves
    public float moveSpeed;

    //If the leg is already moving to a new position
    private bool targetIsMoving = false;

    //bool for the hop
    private bool hopOn;

    //The current location of the last knee (it always moves to the last knee so the last leg stays upright, which looks prettier)
    public Transform currentTarget;

    //A new target for the leg to move to so it doesn't glide
    public Transform newTarget;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 15;
    }

    // Update is called once per frame
    void Update()
    {
        //A vector3 just above the target on the ground, so that the knee is located instead of the feet
        Vector3 slightlyHigher = new Vector3(target.transform.position.x, target.transform.position.y + 3f, target.transform.position.z);

        //Put the target on the ground, regardless of surface height
        //TODO: ignore legs colliders, so the legs can have colliders
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 5f))
        {
            target.transform.position = hit.point;
            //Debug.Log(hit.point);
        }

        //5 or 5.5f is pretty far
        //Debug.Log(Vector3.Distance(target.transform.position, feetEnd.transform.position));

        //If the distance between the target and the feet is far enough; move the leg
        if(Vector3.Distance(target.transform.position, feetEnd.transform.position) > 3.5f && targetIsMoving == false)
        {
            newTarget.position = slightlyHigher;
            targetIsMoving = true;
            hopOn = false;
        }

        Debug.Log(Vector3.Distance(currentTarget.position, newTarget.position));

        if (targetIsMoving)
        {
            //Slowly move the leg to its new location
            //currentTarget.position = Vector3.Lerp(currentTarget.position, newTarget, moveSpeed);

            //this makes for a little hop
            if (hopOn == false)
            {
                currentTarget.position += new Vector3(newTarget.position.x - currentTarget.position.x, newTarget.position.y + 2 - currentTarget.position.y, newTarget.position.z - currentTarget.position.z).normalized * moveSpeed * Time.deltaTime;

                if (Vector3.Distance(currentTarget.position, newTarget.position) < 2f)
                    hopOn = true;
            }

            if (hopOn == true)
                currentTarget.position += (newTarget.position - currentTarget.position).normalized * moveSpeed * Time.deltaTime;

        }

        //if the leg is close enough, stop moving
        if(Vector3.Distance(newTarget.position, currentTarget.position) <= 0.3f)
        {
            currentTarget.position = newTarget.position;
            targetIsMoving = false;
        }
    }
}
