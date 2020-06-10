using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPickup : MonoBehaviour
{
    private bool pickedUp;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && pickedUp == false)
        {
            other.GetComponentInChildren<Gun>().buildCount = 3;
            transform.GetChild(1).gameObject.SetActive(false);
            pickedUp = true;
        }
    }

    public void Reset()
    {
        pickedUp = false;
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
