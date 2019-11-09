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
            jump();
        }
    }




    void stepMove(float axis)
    {
        rb.AddRelativeForce(Vector3.forward * speed * axis);
        c_s.updateValues(new Vector3Int(1, 0, 0));
    }

    void stepTurn(float axis)
    {
        rb.AddTorque(Vector3.up * rotationSpeed * axis, ForceMode.Impulse);
        c_s.updateValues(new Vector3Int(0, 1, 0));

    }

    void jump()
    {
        rb.AddRelativeForce(Vector3.up * jumpSpeed * Input.GetAxisRaw("Jump"), ForceMode.Impulse);
        c_s.updateValues(new Vector3Int(0,0,1));
    }

    
}
