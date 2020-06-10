using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoAnim : MonoBehaviour
{
    public GameObject ammoExChild;
 
    // Update is called once per frame
    void Update()
    {
        ammoExChild.transform.localRotation *= Quaternion.Euler(new Vector3(0, 60 * Time.deltaTime, 0));
    }
}
