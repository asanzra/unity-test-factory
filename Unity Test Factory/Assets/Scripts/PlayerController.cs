using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    void Update()
    {
        Debug.Log(GetComponent<Rigidbody>().velocity);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        // Normalize the movement vector to prevent faster diagonal movement
        movement.Normalize();

        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForce, 0));
        }

        // Apply movement to the Rigidbody
        GetComponent<Rigidbody>().AddForce(movement * moveSpeed + new Vector3(0f, GetComponent<Rigidbody>().velocity.y, 0f));
        
    }
}
