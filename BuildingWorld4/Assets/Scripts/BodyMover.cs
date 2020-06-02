using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMover : MonoBehaviour
{
    public Transform BodyTarget;

    public float moveSpeed = 0.1f;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 TargetPos = new Vector3(BodyTarget.position.x, transform.position.y, BodyTarget.position.z);
        if(Mathf.Pow(Mathf.Pow((transform.position.x - BodyTarget.position.x), 2) + Mathf.Pow((transform.position.z - BodyTarget.position.z), 2), 0.5f) > 5)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, moveSpeed);
        }
    }
}
