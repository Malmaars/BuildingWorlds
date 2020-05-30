using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    //Ignore the legs, we can't step on legs
    public LayerMask IgnoreMe;

    //The Foot of the leg
    public Transform bottomLeg;

    //The position the leg needs to move to
    private Vector3 currentTarget;

    //the position the leg was standing in
    private Vector3 oldPos;

    //The distance between the old position and the target
    private float distanceOld;

    //Check if the leg is already moving
    private bool isMoving = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //shoot out a raycast and check if it hits anything
        RaycastHit hit;
        if(Physics.Raycast(new Vector3(transform.position.x,transform.position.y + 3, transform.position.z), -Vector3.up, out hit, 10f, ~IgnoreMe))
        {
            //make sure we're always on the ground
            transform.position = hit.point;
            //Debug.Log(Vector3.Distance(hit.point, bottomLeg.position));

            //Use the formula to calculate the difference between two points, because we want a different movement between the x and the z axis.
            //And check if the leg is already moving
            if(((hit.point.x - bottomLeg.position.x) * (hit.point.x - bottomLeg.position.x) > 5f*5f ||
                (hit.point.z - bottomLeg.position.z) * (hit.point.z - bottomLeg.position.z) > 3f*3f)
                && isMoving == false)
            {
                //make the current location of the foot the old position
                oldPos = bottomLeg.position;

                //make the target the current location of the raycast
                currentTarget = hit.point;

                //calculate and save the initial distance between the old position and the target. Only on the z and x axis
                //Instead of using the Mathf.Sqrt function, we use the Pow function with a power to 0.5f, because that's the same as a square root. We do this because the Pow function works better
                distanceOld = Mathf.Pow(Mathf.Pow((currentTarget.x - oldPos.x), 2) + Mathf.Pow((currentTarget.z - oldPos.z), 2), 0.5f);

                //We are now moving
                isMoving = true;
            }
        }

        //If we are moving
        if (isMoving == true)
        {
            //the height the leg will raise during steps. Initialise to 0, because we need to initialise it
            float stepHeight;

            //Calculate the difference between the height of the target and the height of the old position. Add 2. Now we have a height that looks good on all angles
            stepHeight = Mathf.Pow(Mathf.Pow((currentTarget.y - oldPos.y), 2), 0.5f) + 5;

            //Continuously calculate the distance between the target and the current position of the foot, but only on the x and z axis
            float distanceFromT = Mathf.Pow(Mathf.Pow((currentTarget.x - bottomLeg.position.x), 2) + Mathf.Pow((currentTarget.z - bottomLeg.position.z), 2), 0.5f);

            //If we are on the first half of the journey, raise the leg
            if (distanceFromT >= distanceOld/2)
            { 
                Vector3 halfWay = new Vector3(currentTarget.x, stepHeight, currentTarget.z);
                // move leg upward
                bottomLeg.position = Vector3.MoveTowards(bottomLeg.position, halfWay, 20f * Time.deltaTime);
            }

            //if We are on the second half of the journey, move the leg back down again
            if (distanceFromT < distanceOld/2)
            {
                // move leg downward
                bottomLeg.position = Vector3.MoveTowards(bottomLeg.position, currentTarget, 20f * Time.deltaTime);
            }


            //Debug.Log(Vector3.Distance(currentTarget, bottomLeg.position));

            //If the current position of the foot is close enough to the target, we stop moving
            if (Vector3.Distance(currentTarget, bottomLeg.position) < 0.1f)
                isMoving = false;
        }
    }
}
