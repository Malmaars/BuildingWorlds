using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullets : MonoBehaviour
{
    public GameObject Bullet;

    public Transform Barrel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject temp = Instantiate(Bullet, Barrel.transform.position, this.transform.rotation);
        temp.GetComponent<Rigidbody>().AddForce(Barrel.transform.forward * 40, ForceMode.Impulse);
    }
}
