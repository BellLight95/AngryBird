using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{ 
    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Bullet");
        if (target != null)
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z-0.2f);
        }
        
        
    }
}
