using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform target; 
    private Vector3 cameraTarget; 

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    // Update is called once per frame
    void Update()
    {
         cameraTarget = new Vector3(target.position.x,transform.position.y,target.position.y); 
        transform.position = Vector3.Lerp(transform.position, cameraTarget,Time.deltaTime*20);
    }
}
