using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowUp : MonoBehaviour
{
    public float growSpeed;
    private bool buildingDone = false;
    public GameObject Building;
    public GameObject DoneBuilding;

    public GameObject MyBuilding;
    // Start is called before the first frame update
    void Awake()
    {
        this.transform.localScale = Vector3.zero;
        growSpeed = 0.05f;
        MyBuilding = Instantiate(Building, this.transform.GetChild(0).position, this.transform.rotation, this.transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.transform.localScale.x < 1)
            this.transform.localScale = new Vector3(Mathf.Lerp(this.transform.localScale.x, 1.1f, growSpeed), Mathf.Lerp(this.transform.localScale.y, 1.1f, growSpeed), Mathf.Lerp(this.transform.localScale.z, 1.1f, growSpeed));

        if (this.transform.localScale.x >= 1)
        {
            if (buildingDone == false)
            {
                Destroy(MyBuilding);
                Instantiate(DoneBuilding, this.transform.GetChild(0).position, this.transform.rotation);
                buildingDone = true;
            }
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
