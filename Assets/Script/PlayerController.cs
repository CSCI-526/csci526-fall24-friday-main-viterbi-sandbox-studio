using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;        
    public float jumpForce = 5f;    
    private Rigidbody rb;           
    private bool isGrounded = true;
    private bool isBlocked = false;


    public GameObject arrowPrefab;   
    private GameObject arrowInstance; 

    private CharacterSwitcher characterSwitcher;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterSwitcher = FindObjectOfType<CharacterSwitcher>();

        
        arrowInstance = Instantiate(arrowPrefab, transform.position + Vector3.up * 3, Quaternion.identity);
        arrowInstance.transform.SetParent(transform);  
        arrowInstance.SetActive(false);  
    }

    void Update()
    {
        
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = Vector3.zero;

        if (isBlocked)
        {
            if (verticalInput < 0) 
            {
                movement = new Vector3(verticalInput, 0, -horizontalInput) * speed * Time.deltaTime;
            }
        }
        else
        {
            
            if (verticalInput > 0) 
            {
                movement = new Vector3(verticalInput, 0, -horizontalInput) * speed * Time.deltaTime;
            }
            else if (verticalInput <= 0) 
            {
                movement = new Vector3(verticalInput, 0, -horizontalInput) * speed * Time.deltaTime;
            }

            
            if (horizontalInput != 0)
            {
                movement = new Vector3(verticalInput, 0, -horizontalInput) * speed * Time.deltaTime;
            }
        }

        transform.Translate(movement);

        
        

        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; 
        }
    }

    void FixedUpdate()
    {
        if (isBlocked)
        {

            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Tile"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Untagged"))
        {
            isBlocked = true;
        }
    }
    
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            isBlocked = false;
        }

        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Tile"))
        {
            isGrounded = false;
        }
    }

    
    public void ToggleArrow(bool showArrow)
    {
        if (arrowInstance != null)
        {
            arrowInstance.SetActive(showArrow);
        }
    }
}