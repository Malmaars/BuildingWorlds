using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Death;
    private bool canDie = false;
    // Start is called before the first frame update
    void Start()
    {
        Death = this.gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Death.GetComponent<ParticleSystem>().isPlaying == false && canDie == true)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "MainCamera")
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            this.transform.GetChild(0).gameObject.SetActive(false);
            Death.transform.forward = this.transform.up * -1;
            Death.GetComponent<ParticleSystem>().Play();
            canDie = true;

        }
    }
}
