﻿using System.Collections;
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

    Canvas_Script c_s;


    void Start()
    {
        charContr = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        c_s = FindObjectOfType<Canvas_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            stepMove(Input.GetAxisRaw("Vertical"));
            stepTurn(Input.GetAxisRaw("Horizontal"));
            jump(Input.GetAxisRaw("Jump"));
        }
    }




    void stepMove(float axis)
    {
        rb.AddRelativeForce(Vector3.forward * speed * axis);
        if(axis!=0)
            c_s.updateValues(1,0,0);
    }

    void stepTurn(float axis)
    {
        rb.AddTorque(Vector3.up * rotationSpeed * axis, ForceMode.Impulse);
        if (axis != 0)
            c_s.updateValues(0,1,0);

    }

    void jump(float axis)
    {
        rb.AddRelativeForce(Vector3.up * jumpSpeed * axis, ForceMode.Impulse);
        if (axis != 0)
            c_s.updateValues(0, 0, 1);
    }

    
}
