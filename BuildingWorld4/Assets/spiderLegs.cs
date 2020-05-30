using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderLegs : MonoBehaviour
{
    private List<Transform> Legs;
    // Start is called before the first frame update
    void Awake()
    {
        this.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
