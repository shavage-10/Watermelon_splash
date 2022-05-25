using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 10f;

    private float xInput;
    public float zInput;

    public Text distancemoved;
    float distanceunit = 0; 


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("distance", 0, 1 / moveSpeed);


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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            
        }
    }

    void distance()
    {
        distanceunit = distanceunit + 1;
        distancemoved.text = distanceunit.ToString();
    }
}
