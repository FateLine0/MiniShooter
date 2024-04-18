using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    public Camera mainCamera; 
    public GameObject Player;
    
    public float speed; 
    public GameObject SpherePrefab;  

    Rigidbody rb;  
    
    //Transform cameraTransform; 
    Vector3 moveDirection ; 

    Vector3 mousePosition; 
    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody>();
        //cameraTransform = Player.transform.GetChild(0).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

         moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        rb.AddForce(moveDirection*speed*Time.deltaTime); 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit ; 

        if(Physics.Raycast(ray, out hit , 100)){
                mousePosition = hit.point; 
        } 

        Vector3 lookdDir = mousePosition - transform.position;
        lookdDir.y = 0f;
        transform.LookAt(lookdDir+transform.position,Vector3.up); 
  
       /* if(Input.GetKey(KeyCode.A)){
            Debug.Log("A"); 
            rb.AddForce((-Player.transform.right) * speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.W)){
            Debug.Log("B"); 
            rb.AddForce(Player.transform.up * speed * Time.deltaTime);
        } 
        if(Input.GetKey(KeyCode.S)){
            Debug.Log("C"); 
            rb.AddForce((-Player.transform.up)* speed * Time.deltaTime);
        } 
        if(Input.GetKey(KeyCode.D)){
            Debug.Log("D"); 
            rb.AddForce(Player.transform.right * speed * Time.deltaTime);
        }
        */

        if(Input.GetMouseButtonDown(0)){
            GameObject sphere = Instantiate(SpherePrefab, Player.transform.position+Player.transform.forward, Quaternion.identity);
            Rigidbody sphereRb = sphere.GetComponent<Rigidbody>(); 
            sphereRb.AddForce(Player.transform.forward*1000);
         }
    }

    void FixedUpdate(){
        if (moveDirection != Vector3.zero)
        {
            Player.transform.Translate(moveDirection * speed * Time.fixedDeltaTime, Space.World);
        }

    }
}
