using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public GameObject Broken;
    public Transform breakLoc;

    public void breakThis()
    {
        Instantiate(Broken, this.transform.position, this.transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                
                rb.AddExplosionForce(800f, breakLoc.position, 10f);
            }
        }
        Destroy(this.gameObject);
    }
}
