using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKMovement : MonoBehaviour
{
    public Transform lookPoint;
    public Transform backPoint;

    public bool backEnd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(lookPoint);

        if (backEnd == false)
        {
            if (Vector3.Distance(backPoint.position, transform.position) > 1f)
            {
                Vector3 direction = transform.position - backPoint.position;
                direction.Normalize();
                transform.position -= direction * 5 * Time.deltaTime;
            }

/*            if(Vector3.Distance(backPoint.position, transform.position) < 1f)
            {
                Vector3 direction = transform.position - backPoint.position;
                direction.Normalize();
                transform.position += direction * 5 * Time.deltaTime;
            }*/

            if (Vector3.Distance(lookPoint.position, transform.position) < 1f)
            {
                Vector3 direction = transform.position - lookPoint.position;
                direction.Normalize();
                transform.position += direction * 5 * Time.deltaTime;
            }

            if (Vector3.Distance(lookPoint.position, transform.position) > 1f)
            {
                Vector3 direction = transform.position - lookPoint.position;
                direction.Normalize();
                transform.position -= direction * 5 * Time.deltaTime;
            }
        }
        Debug.Log(Vector3.Distance(lookPoint.position, this.transform.position));
    }
}
