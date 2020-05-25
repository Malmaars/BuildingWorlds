using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public GameObject wall;

    public Camera fpsCam;
    public Transform Player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Build();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Build ()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name + hit.point);
            Vector3 hitPoint = hit.point;
            Transform thing = hit.transform;

            if(hitPoint.z <= thing.position.z - (thing.localScale.z / 2))
            {
                Debug.Log("Front");
            }

            if (hitPoint.z >= thing.position.z + (thing.localScale.z / 2))
            {
                Debug.Log("Back");
            }

            if (hitPoint.y <= thing.position.y - (thing.localScale.y / 2))
            {
                Debug.Log("Bottom");
            }

            if (hitPoint.y >= thing.position.y + (thing.localScale.y / 2))
            {
                Debug.Log("Top");
                GameObject temp = Instantiate(wall);
                temp.transform.position = hitPoint;
                temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y + temp.transform.localScale.y / 2, temp.transform.position.z);
                //temp.transform.up = thing.transform.up;
                temp.transform.rotation = Player.transform.rotation;
            }

            if (hitPoint.x <= thing.position.x - (thing.localScale.x / 2))
            {
                Debug.Log("Left");
            }

            if (hitPoint.x >= thing.position.x + (thing.localScale.x / 2))
            {
                Debug.Log("Right");
            }

        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("Pew");
        }
    }
}
