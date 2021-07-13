using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    Vector3 targetPosition;
    Vector3 originPosition;
    bool Finish = false;
    void Start()
    {
        originPosition = transform.position;
        targetPosition = new Vector3(originPosition.x+3,originPosition.y,originPosition.z);              
    }

    // Update is called once per frame
    void Update()
    {
        if(Finish != true)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
            if(transform.position == Vector3.Lerp(transform.position,targetPosition,0.1f))
            {
                Finish = true;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, originPosition, 0.1f);
            if(transform.position == Vector3.Lerp(transform.position,originPosition,0.1f))
            {
                Finish = false;
            }
        }                
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.transform.name == "Player")
        //{
        //    collision.transform.parent = this.transform;
            
        //}
    }
}
