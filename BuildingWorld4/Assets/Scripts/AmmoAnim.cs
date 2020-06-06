using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoAnim : MonoBehaviour
{
    //public Ammo ammo;

    public GameObject ammoExChild;

/*    public GameObject ammoExample;
    public GameObject fullBody;
    public GameObject brokenBody;
    public GameObject Hologram;*/

    // Start is called before the first frame update
    void Start()
    {
/*        ammoExample = ammo.ammoExample;
        fullBody = ammo.fullBody;
        brokenBody = ammo.brokenBody;
        Hologram = ammo.hologram;

        ammoExChild = Instantiate(ammoExample, this.transform, false);*/
    }

    // Update is called once per frame
    void Update()
    {
        ammoExChild.transform.localRotation *= Quaternion.Euler(new Vector3(0, 60 * Time.deltaTime, 0));
    }
}
