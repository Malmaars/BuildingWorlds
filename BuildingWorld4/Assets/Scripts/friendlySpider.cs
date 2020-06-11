using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friendlySpider : MonoBehaviour
{
    public Transform player;
    public Transform playerCamera;
    public Transform[] eyes;
    public float moveSpeed;

    private bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform eye in eyes)
        {
            transform.rotation = Quaternion.LookRotation(playerCamera.position);
        }
        transform.LookAt(playerCamera.position);
        float distancefromPlayer = Mathf.Pow(Mathf.Pow((transform.position.x - player.position.x), 2) + Mathf.Pow((transform.position.z - player.position.z), 2), 0.5f);
        if (distancefromPlayer > 20f && distancefromPlayer < 50f && canMove)
        {
            transform.position = transform.position + (transform.forward * Time.deltaTime * moveSpeed);
        }

        GetComponent<AudioSource>().volume = 1 / distancefromPlayer;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f, LayerMask.NameToLayer("Player")))
        {
            float distancetoRay = Vector3.Distance(transform.position, hit.point);
            if(distancetoRay < distancefromPlayer)
            {
                canMove = false;
            }

            if (distancetoRay >= distancefromPlayer)
            {
                canMove = true;
            }
        }
    }
}
