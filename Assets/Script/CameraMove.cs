using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject PlayerCameraPosition;
    float Distancetravel = 0f;
    bool Calculation = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveposition = new Vector3(this.transform.position.x, PlayerCameraPosition.transform.position.y, this.transform.position.z);

        if (PlayerControl.Instance.isjump == false&&this.transform.position.y<moveposition.y)
        {           
            this.transform.position = Vector3.Lerp(this.transform.position, moveposition,0.1f);                        
        }
        

        
        
      
    }
}
