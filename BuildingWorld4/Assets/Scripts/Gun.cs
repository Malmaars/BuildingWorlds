using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public int damage = 1;
    public float range = 100f;
    public float fireRate = 100f;
    public GameObject wall;
    public GameObject hologram;
    public LayerMask enemyLayer;
    public Material seeThrough;
    public Material badSeeThrough;

    private GameObject wallTemp;

    public Image[] buildMeter;
    public Sprite fullBuild;
    public Sprite emptyBuild;
    public int buildCount = 0;

    public Camera fpsCam;
    public Transform Player;
    public ParticleSystem MuzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        //Check if we hit something
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if (Input.GetButtonDown("Fire2"))
            {
                //Maak hologram
                wallTemp = Instantiate(hologram, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            }

            if (Input.GetButton("Fire2"))
            {
                //Check met hologram of plaatsing kan
                if (hit.transform.gameObject != wallTemp.transform.GetChild(0).gameObject)
                {
                    wallTemp.transform.position = hit.point;
                    wallTemp.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    wallTemp.transform.rotation *= Quaternion.LookRotation(Player.forward);

                    if (wallTemp.transform.GetChild(0).GetComponent<HologramTriggerCheck>().inSomething == true || buildCount == 0)
                    {
                        MeshRenderer[] wallMats = wallTemp.transform.GetComponentsInChildren<MeshRenderer>();
                        foreach (MeshRenderer mat in wallMats)
                        {
                            mat.material = badSeeThrough;
                        }
                    }

                    if (wallTemp.transform.GetChild(0).GetComponent<HologramTriggerCheck>().inSomething == false && buildCount > 0)
                    {
                        MeshRenderer[] wallMats = wallTemp.transform.GetComponentsInChildren<MeshRenderer>();
                        foreach (MeshRenderer mat in wallMats)
                        {
                            mat.material = seeThrough;
                        }
                    }
                }

            }

            if (Input.GetButtonUp("Fire2"))
            {
                Destroy(wallTemp);
                if (wallTemp.transform.GetChild(0).GetComponent<HologramTriggerCheck>().inSomething == false && buildCount > 0)
                    Build(hit);
                //Als plaatsing kan, plaats
            }
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            //Make sure we can't really shoot every frame
            nextTimeToFire = Time.time + 1f / fireRate;
            MuzzleFlash.Play();
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
                Shoot(hit);
        }

        for (int i = 0; i < buildMeter.Length; i++)
        {
            if(buildCount >= i+1)
            {
                buildMeter[i].sprite = fullBuild;
            }

            if(buildCount < i+1)
            {
                buildMeter[i].sprite = emptyBuild;
            }
        }
    }
    void Build (RaycastHit hit)
    {
        //Debug.Log(hit.transform.name + hit.point);

        //Only build on stuff that is considered environment
        if (hit.transform.gameObject.tag == "Environment" || hit.transform.gameObject.tag == "Breakable")
        {
            //Construct a wall on the location and have the rotation based on the normal of the object hit
            GameObject temp = Instantiate(wall, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            temp.transform.rotation *= Quaternion.LookRotation(Player.forward);
            buildCount--;
        }
    }

    void Shoot(RaycastHit hit)
    {
        //Play the animation for the muzzle flash

        if (hit.transform.GetComponent<Enemy>() != null)
        {
            hit.transform.GetComponent<Enemy>().health -= damage;
        }

        //Debug.Log("Pew");
        if (hit.transform.parent != null)
        {
            //If the object is breakable, break it
            if (hit.transform.parent.gameObject.tag == "Breakable" && hit.transform.root.localScale == new Vector3(1, 1, 1))
            {
                Transform temp = hit.transform.root;
                //Replace the object by the broken version

                temp.GetComponent<Break>().breakThis();
            }
        }

        //If we hit an object with a rigidbody, we add some force to it.
        if (hit.transform.GetComponent<Rigidbody>() != null)
        {
            hit.transform.GetComponent<Rigidbody>().AddForce(fpsCam.transform.forward * 10, ForceMode.Impulse);
        }

        //Play an animation on the location we hit
        Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
    }
}
