using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashTime; 
    public float dashSpeed; 

    Vector3 dashDirection; 

    private Rigidbody rb;

    private PlayerController pm;  
    private KeyCode dash = KeyCode.E;  

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        float posX = Input.GetAxis("Horizontal");
        float posZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(posX,0.0f , posZ);
        if(moveDirection!=Vector3.zero){
                dashDirection = moveDirection; 

        }
        if(Input.GetKeyDown(dash)){
            StartCoroutine(movementDash()); 
        }
    }   

    IEnumerator movementDash(){
         float startTime = Time.time; 
    while (Time.time < startTime + dashTime)
    { 
        rb.AddForce(dashDirection * dashSpeed, ForceMode.Impulse); // Apply the force as an impulse
        yield return null;
    }
    }
}
