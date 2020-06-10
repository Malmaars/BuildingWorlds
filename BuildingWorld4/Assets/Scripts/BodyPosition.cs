using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPosition : MonoBehaviour
{
    //All the legs on the body. You'll need to manually add these in the unity editor. We want the feet.
    public Transform[] Legs;

    //lists with specific sets of legs
    public Transform[] rightLegs;
    public Transform[] leftLegs;
    public Transform[] frontLegs;
    public Transform[] backLegs;

    //The offset we want to use in the height calculation
    public float offset;

    private float scaler;

    // Start is called before the first frame update
    void Awake()
    {
        scaler = transform.parent.localScale.x;
        offset *= scaler;
    }

    // Update is called once per frame
    void Update()
    {
        //The height of the body is calculated by the average height of the legs + an offset
        float averageHeight = 0;

        //For each leg we add their height to the average value
        for(int i = 0; i < Legs.Length; i++)
        {
            averageHeight += Legs[i].position.y;
        }

        //Now we divide the total with the number of legs so we actually get the average
        averageHeight = averageHeight / Legs.Length;

        //Now we edit the position to the values we calculated
        transform.position = new Vector3(transform.position.x, averageHeight + offset, transform.position.z);


        //Let's edit the z rotation
        //the average for the right and left left heights
        float rightHeight = 0;
        float leftHeight = 0;

        //Get the average height for the right Legs
        foreach(Transform Leg in rightLegs)
        {
            rightHeight += Leg.position.y;
        }
        rightHeight = rightHeight / rightLegs.Length;

        //Do the same for the left Legs
        foreach(Transform Leg in leftLegs)
        {
            leftHeight += Leg.position.y;
        }
        leftHeight = leftHeight / leftLegs.Length;

        //Now the x rotation
        float frontHeight = 0;
        float backHeight = 0;

        //Get the average height for the front Legs
        foreach (Transform Leg in frontLegs)
        {
            frontHeight += Leg.position.y;
        }
        frontHeight = frontHeight / frontLegs.Length;

        //Do the same for the back Legs
        foreach (Transform Leg in backLegs)
        {
            backHeight += Leg.position.y;
        }
        backHeight = backHeight / backLegs.Length;

        float rotationStrength = 3f;
        //Right minus Left, Back minus Front
        transform.rotation = Quaternion.Euler((backHeight - frontHeight) * rotationStrength, transform.eulerAngles.y, (rightHeight - leftHeight) * rotationStrength);

    }
}
