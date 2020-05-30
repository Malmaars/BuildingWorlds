using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class growDown : MonoBehaviour
{
    public float growSpeed;
    public float timer;
    public List<GameObject> Kids;

    // Start is called before the first frame update
    void Awake()
    {
        Kids = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Kids.Add(transform.GetChild(i).gameObject);
        }
        growSpeed = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 6)
        {
            foreach(GameObject kid in Kids)
            kid.transform.localScale = Vector3.Lerp(kid.transform.localScale, new Vector3(0, 0, 0), growSpeed);
        }

        if (transform.GetChild(0).transform.localScale.x <= 0.01f)
            Destroy(this.gameObject);
    }
}
