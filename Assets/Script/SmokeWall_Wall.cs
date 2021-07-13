using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeWall_Wall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 playerdis = collision.transform.position;
        Vector3 mydis = transform.position;

        Vector3 dis = playerdis - mydis;
        float distance = dis.magnitude;        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 playerdis = other.transform.position;
            Vector3 mydis = transform.position;

            Vector3 dis = playerdis - mydis;
            float distance = dis.magnitude;

            if (distance < 1f)
            {
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z);
            }
        }        
    }
}
