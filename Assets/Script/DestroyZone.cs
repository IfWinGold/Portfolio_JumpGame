using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "BOX")
        {            
            Destroy(other.gameObject);
        }        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag != "BOX")
        {            
            Destroy(collision.gameObject);
        }
        
    }
}
