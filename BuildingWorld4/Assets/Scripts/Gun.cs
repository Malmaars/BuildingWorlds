using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage = 1;
    public float range = 100f;
    public float fireRate = 100f;
    public GameObject wall;
    public GameObject brokenWall;
    public LayerMask enemyLayer;

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
            //Make sure we can't really shoot every frame
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Build ()
    {
        RaycastHit hit;

        //Check if we hit something
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name + hit.point);

            //Only build on stuff that is considered environment
            if (hit.transform.gameObject.tag == "Environment")
            {
                //Construct a wall on the location and have the rotation based on the normal of the object hit
                Instantiate(wall, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                Debug.Log(hit.normal);
            }
        }
    }

    void Shoot()
    {
        //Play the animation for the muzzle flash
        MuzzleFlash.Play();

        RaycastHit hit;
        //Check if we hit something
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (hit.transform.GetComponent<Enemy>() != null)
            {
                hit.transform.GetComponent<Enemy>().health -= damage;
            }

            //Debug.Log("Pew");

            if(hit.transform.parent != null)
            {
                //If the object is breakable, break it
                if (hit.transform.parent.gameObject.tag == "Breakable" && hit.transform.parent.localScale == new Vector3(1, 1, 1))
                {
                    //Replace the object by the broken version
                    Instantiate(brokenWall, hit.transform.parent.position, hit.transform.parent.rotation);
                    Destroy(hit.transform.parent.gameObject);
                }
            }

            //If we hit an object with a rigidbody, we add some force to it.
            if(hit.transform.GetComponent<Rigidbody>() != null)
            {
                hit.transform.GetComponent<Rigidbody>().AddForce(fpsCam.transform.forward * 10, ForceMode.Impulse);
            }

            //Play an animation on the location we hit
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
