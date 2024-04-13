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

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.y = 0f; // Wir setzen die Z-Koordinate auf 0, da wir uns in einer 2D-Szene befinden

        // Die Richtung vom Spieler zur Maus berechnen
        Vector3 direction = mousePosition - transform.position;
        direction.Normalize();

        // Die Rotation des Spielers setzen, um zur Maus zu zeigen
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        

        if(Input.GetMouseButtonDown(0)){
            GameObject sphere = Instantiate(SpherePrefab, Player.transform.position+Player.transform.forward, Quaternion.identity);
            Rigidbody sphereRb = sphere.GetComponent<Rigidbody>(); 
            sphereRb.AddForce(Player.transform.forward*1000);
         }
    }
}
