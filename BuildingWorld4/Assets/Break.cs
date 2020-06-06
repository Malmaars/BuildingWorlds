using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public GameObject Broken;

    public void breakThis()
    {
        Instantiate(Broken, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
