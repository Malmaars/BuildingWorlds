using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowUp : MonoBehaviour
{
    public float growSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        this.transform.localScale = Vector3.zero;
        growSpeed = 0.2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.transform.localScale.x < 1)
            this.transform.localScale = new Vector3(Mathf.Lerp(this.transform.localScale.x, 1, growSpeed), Mathf.Lerp(this.transform.localScale.y, 1, growSpeed), Mathf.Lerp(this.transform.localScale.z, 1, growSpeed));

        if (this.transform.localScale.x >= 1)
            this.transform.localScale = new Vector3(1, 1, 1);
    }
}
