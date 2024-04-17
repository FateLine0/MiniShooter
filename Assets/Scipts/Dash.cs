using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEditor.Compilation;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashTime; 
    public float dashSpeed; 

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
        if(Input.GetKeyDown(dash)){
            StartCoroutine(movementDash()); 
        }
    }   

    IEnumerator movementDash(){
         float startTime = Time.time; 
    Vector3 originalVelocity = rb.velocity.normalized; // Normalize to keep direction but ignore current speed
    rb.velocity = Vector3.zero; // Optional: Reset velocity to start fresh with the dash

    while (Time.time < startTime + dashTime)
    { 
        rb.AddForce(originalVelocity * dashSpeed, ForceMode.Impulse); // Apply the force as an impulse
        yield return null;
    }

    // Optional: Reset the velocity after the dash
    rb.velocity = originalVelocity * (rb.velocity.magnitude / dashSpeed); // Slow down to original speed
    }
}
