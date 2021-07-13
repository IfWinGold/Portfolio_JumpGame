using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeWall : MonoBehaviour
{
    bool isActive = false;
    GameObject Wall;
    BoxCollider boxcollider;
    Renderer renderer;
    Color originColor;    
    void Start()
    {
        Wall = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Wall.activeSelf == false)
        {
            isActive = false;
            StartCoroutine(DestroyCollision());
        }
        else
        {
            isActive = true;
            StartCoroutine(DestroyCollision());
        }
    }

    public IEnumerator DestroyCollision()
    {
        if(isActive == true)
        {
            yield return new WaitForSeconds(3f);
            Wall.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(3f);
            Wall.SetActive(true);
        }
    }


}
