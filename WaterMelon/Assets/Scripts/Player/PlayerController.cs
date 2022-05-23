using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 10f;

    private float xInput;
    public float zInput;
   
   
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        
    }

    // Update is called once per frame
    void Update()
    {
        //input and animations
        ProcessInputs();

    }

    private void FixedUpdate()
    {
        //Movement
        Move();
    }

    private void ProcessInputs()
    {
        xInput = Input.acceleration.x;
    }

    private void Move()
    {
        rb.AddForce(new Vector3(xInput, 0f, zInput) * moveSpeed);
    }
}
