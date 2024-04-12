using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    public Camera mainCamera; 
    public GameObject Player;
    
    public float speed; 

    public GameObject SpherePrefab;  

    Rigidbody rb; 

    public float mouseSensitive; 
    
    Transform cameraTransform; 

    private Quaternion targetRotation; 

    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody>();
        cameraTransform = Player.transform.GetChild(0).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            Player.transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        }

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

         Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 mousePosition = ray.GetPoint(rayDistance);

            // Drehe den Spieler zur Mauszeigerposition
            Vector3 direction = mousePosition - transform.position;
            direction.y = 0; // Damit die Rotation nur um die Y-Achse erfolgt
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Player.transform.rotation = lookRotation;
        }
         

        if(Input.GetMouseButtonDown(0)){
            GameObject sphere = Instantiate(SpherePrefab, Player.transform.position+Player.transform.forward, Quaternion.identity);
            Rigidbody sphereRb = sphere.GetComponent<Rigidbody>(); 
            sphereRb.AddForce(Player.transform.forward*1000);
         }
    }
}
