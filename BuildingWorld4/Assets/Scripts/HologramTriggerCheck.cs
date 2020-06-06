using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramTriggerCheck : MonoBehaviour
{
    public bool inSomething = false;

    public void OnTriggerStay(Collider other)
    {
        inSomething = true;
        Debug.Log("NO");
    }

    private void OnTriggerExit(Collider other)
    {
        inSomething = false;
    }
}
