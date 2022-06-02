using Jesper.Scoreboards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Scoreboard scoreboard;
    [SerializeField] private AudioSource source;

    public Rigidbody rb;
    public float moveSpeed = 10f;
    private float xInput;
    public float zInput;
    public float threshold = -20f;
    public Text distancemoved;
    public int distanceunit = 0;

    

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("distance", 0, 1 / moveSpeed);
        AudioManager.Instance.musicSource = source;
    }
    // Update is called once per frame
    void Update()
    {
        if (rb.transform.position.y < threshold)
        {
            PlayerManager.gameOver = true;
        }


    }

    private void FixedUpdate()
    {
        //Movement
        ProcessInputs();
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            ScoreboardEntryData entry = new ScoreboardEntryData();
            entry.entryScore = distanceunit;
            entry.entryName = "Player";
            scoreboard.Addentry(entry);
        }
    }

    public void distance()
    {
        distanceunit = distanceunit + 1 ;
        
        
        distancemoved.text = distanceunit.ToString();
    }
}
