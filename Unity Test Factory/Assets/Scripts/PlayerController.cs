using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    void Start()
    {
        
    }

    [SerializeField] public float moveSpeed = 25f;
    [SerializeField] public float gravity = 9.8f;
    [SerializeField] public float jumpForce = 9.8f * 2;
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector2 speed = new Vector2(horizontalInput, verticalInput);
        speed.Normalize();
        speed *= moveSpeed;

        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        velocity.x = speed.x;
        velocity.z = speed.y;
        velocity.y += -gravity * Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
            velocity.y += jumpForce;

        GetComponent<Rigidbody>().velocity = velocity;
    }
}
