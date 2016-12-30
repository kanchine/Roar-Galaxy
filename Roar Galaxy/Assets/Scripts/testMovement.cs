using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMovement : MonoBehaviour {

    float speed = 0.8f;
    float turnSpeed = 0.5f;
    public Rigidbody rb;


    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }


    void FixedUpdate()
    {
        if (Input.GetButton("Jump"))
        {
            //Spacebar by default will make it move forward
            rb.AddRelativeForce (Vector3.forward*speed); 
        }

        //Rotate camera vertically.
        rb.AddRelativeTorque((Input.GetAxis("Vertical")) * turnSpeed, 0, 0, ForceMode.Acceleration);
        
        //Rotate camera horizontally.
        rb.AddRelativeTorque(0, (Input.GetAxis("Horizontal")) * turnSpeed, 0, ForceMode.Acceleration); 
    }
}
