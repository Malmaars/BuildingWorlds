using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 100f;
    public GameObject wall;
    public GameObject brokenWall;

    public Camera fpsCam;
    public Transform Player;
    public ParticleSystem MuzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Build();
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Build ()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name + hit.point);

            GameObject temp = Instantiate(wall, hit.point, Quaternion.FromToRotation(Vector3.up,hit.normal));
            Debug.Log(hit.normal);
        }
    }

    void Shoot()
    {
        MuzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log("Pew");
            if(hit.transform.parent != null)
            {
                if (hit.transform.parent.gameObject.tag == "Breakable" && hit.transform.parent.localScale == new Vector3(1, 1, 1))
                {
                    Instantiate(brokenWall, hit.transform.parent.position, hit.transform.parent.rotation);
                    Destroy(hit.transform.parent.gameObject);
                }
            }

            if(hit.transform.GetComponent<Rigidbody>() != null)
            {
                hit.transform.GetComponent<Rigidbody>().AddForce(fpsCam.transform.forward * 10, ForceMode.Impulse);
            }
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
