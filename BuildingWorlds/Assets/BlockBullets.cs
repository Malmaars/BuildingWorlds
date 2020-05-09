using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBullets : MonoBehaviour
{
    public GameObject Sword;
    public Transform SwordBlock;

    public Vector3 originalSwordPos;
    public Quaternion originalSwordRot;
    // Start is called before the first frame update
    void Start()
    {
        originalSwordPos = Sword.transform.localPosition;
        originalSwordRot = Sword.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Sword.transform.localRotation = SwordBlock.localRotation;
            Sword.transform.localPosition = SwordBlock.localPosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Sword.transform.localRotation = originalSwordRot;
            Sword.transform.localPosition = originalSwordPos;
        }
    }
}
