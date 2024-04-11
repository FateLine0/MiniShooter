using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Player;
    
    public float speed; 

    public GameObject SpherePrefab;  

    Rigidbody rb; 

    public float mouseSensitive; 
    
    Transform cameraTransform; 

    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody>();
        cameraTransform = Player.transform.GetChild(0).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)){
            Debug.Log("A"); 
            rb.AddForce((-Player.transform.right) * speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.W)){
            Debug.Log("B"); 
            rb.AddForce(Player.transform.forward * speed * Time.deltaTime);
        } 
        if(Input.GetKey(KeyCode.S)){
            Debug.Log("C"); 
            rb.AddForce((-Player.transform.forward)* speed * Time.deltaTime);
        } 
        if(Input.GetKey(KeyCode.D)){
            Debug.Log("D"); 
            rb.AddForce(Player.transform.right * speed * Time.deltaTime);
        }

        if(Input.GetAxis("Mouse X")!= 0){
            cameraTransform.Rotate(Vector3.up *Input.GetAxis("Mouse X")*mouseSensitive ); 
        }
        if(Input.GetMouseButtonDown(0)){
            GameObject sphere = Instantiate(SpherePrefab, Player.transform.position+Player.transform.forward, Quaternion.identity);
            Rigidbody sphereRb = sphere.GetComponent<Rigidbody>(); 
            sphereRb.AddForce(Player.transform.forward*1000);
         }
    }
}
