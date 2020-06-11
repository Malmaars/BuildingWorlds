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

    //How fast we move the legs
    public float moveSpeed = 20f;

    //The Legs that are opposite to this one, we're gonna check if they're grounded or not
    public Transform oppositeLeg1;
    public Transform oppositeLeg2;

    private Vector3 debugVector;
    //check if the correspondend leg is grounded
    bool isGrounded;
    float groundDistance = 0.2f;

    private float scaler;

    public AudioSource audioSRC;
    public AudioClip step;


    // Start is called before the first frame update
    void Awake()
    {
        scaler = transform.root.localScale.x;
        //moveSpeed /= scaler;
        groundDistance *= scaler;
        //We make sure that the leg is on level with the ground when the game begins
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), -Vector3.up, out hit, 100f, ~IgnoreMe))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            bottomLeg.position = new Vector3(bottomLeg.position.x, hit.point.y, bottomLeg.position.z);
        }

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(bottomLeg.position, groundDistance);

        //shoot out a raycast and check if it hits anything
        RaycastHit hit;
        if(Physics.Raycast(new Vector3(transform.position.x,transform.position.y + 1, transform.position.z), -Vector3.up, out hit, 100f, ~IgnoreMe))
        {
            //make sure we're always on the ground
            transform.localPosition = new Vector3(transform.localPosition.x, -0.87f, transform.localPosition.z);
            //Debug.Log(Vector3.Distance(hit.point, bottomLeg.position));

            //Use the formula to calculate the difference between two points, because we want a different movement between the x and the z axis.
            //And check if the leg is already moving
            if(((hit.point.x - bottomLeg.position.x) * (hit.point.x - bottomLeg.position.x) > 5f*5f * scaler ||
                (hit.point.z - bottomLeg.position.z) * (hit.point.z - bottomLeg.position.z) > 3f*3f * scaler) &&
                isMoving == false &&
                oppositeLeg1.GetComponentInChildren<TargetPoint>().isGrounded == true &&
                oppositeLeg2.GetComponentInChildren<TargetPoint>().isGrounded == true)
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
            float stepHeight = 0;

            //Calculate the difference between the height of the target and the height of the old position. Add 2. Now we have a height that looks good on all angles

            if(currentTarget.y >= oldPos.y)
            {
                stepHeight = currentTarget.y + 5 * scaler;
            }

            if (currentTarget.y < oldPos.y)
            {
                stepHeight = oldPos.y + 5 * scaler;
            }

            //Continuously calculate the distance between the target and the current position of the foot, but only on the x and z axis
            float distanceFromT = Mathf.Pow(Mathf.Pow((currentTarget.x - bottomLeg.position.x), 2) + Mathf.Pow((currentTarget.z - bottomLeg.position.z), 2), 0.5f);

            //If we are on the first half of the journey, raise the leg
            if (distanceFromT >= distanceOld/2)
            { 
                Vector3 halfWay = new Vector3(currentTarget.x, stepHeight, currentTarget.z);
                // move leg upward
                bottomLeg.position = Vector3.MoveTowards(bottomLeg.position, halfWay, moveSpeed * Time.deltaTime);
            }

            //if We are on the second half of the journey, move the leg back down again
            if (distanceFromT < distanceOld/2)
            {
                // move leg downward
                bottomLeg.position = Vector3.MoveTowards(bottomLeg.position, currentTarget, moveSpeed * Time.deltaTime);
            }


            //Debug.Log(Vector3.Distance(currentTarget, bottomLeg.position));

            //If the current position of the foot is close enough to the target, we stop moving
            if (Vector3.Distance(currentTarget, bottomLeg.position) < 0.1f * scaler)
            {
                isMoving = false;
                audioSRC.PlayOneShot(step);
            }
        }
    }
}
