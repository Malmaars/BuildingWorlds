using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBullets : MonoBehaviour
{
    public GameObject Sword;
    public Transform SwordBlock;

    public Vector3 originalSwordPos;
    public Quaternion originalSwordRot;
    //public ParticleSystem Clinck;
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
            GetComponent<BoxCollider>().enabled = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Sword.transform.localRotation = originalSwordRot;
            Sword.transform.localPosition = originalSwordPos;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * 40, ForceMode.Impulse);
            other.transform.up = this.transform.forward;
            //Clinck.Play();
        }
    }
}
