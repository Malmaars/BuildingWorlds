using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderAgent : MonoBehaviour
{
    public float range;
    public float moveSpeed;
    public LayerMask myself;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * range, Color.red);
        if(!Physics.Raycast(transform.position, transform.forward, out hit, range, ~myself))
        {
            transform.Translate(-transform.right * Time.deltaTime * moveSpeed);
        }
    }
}
