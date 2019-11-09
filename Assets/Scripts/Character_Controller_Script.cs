using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller_Script : MonoBehaviour
{
    // Start is called before the first frame update


    CharacterController charContr;
    Rigidbody rb;
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;


    void Start()
    {
        charContr = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            stepMove(Input.GetAxisRaw("Vertical"));
            stepTurn(Input.GetAxisRaw("Horizontal"));
            jump();
        }
    }




    void stepMove(float axis)
    {
        rb.AddRelativeForce(Vector3.forward * speed * axis);
    }

    void stepTurn(float axis)
    {
        rb.AddTorque(Vector3.up * rotationSpeed * axis,ForceMode.Impulse);
       
    }

    void jump()
    {
        rb.AddRelativeForce(Vector3.up * jumpSpeed * Input.GetAxisRaw("Jump"), ForceMode.Impulse);
    }

    
}
