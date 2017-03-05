using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpSpeed;

    //ground check
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;

    //Time change
    public bool timeChange; 

    private Rigidbody2D myRigidbody;

    //Animation
    private Animator myAnim;
    public Vector3 respawnPosition;

    //reference to Level Manager
    public LevelManager theLevelManager;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        //sets initial respawn point
        respawnPosition = transform.position;
        //initializing level manager
        theLevelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,whatIsGround);

        //moving left and right
        //going right
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            myRigidbody.velocity = new Vector3(moveSpeed,myRigidbody.velocity.y,0f);
            transform.localScale= new Vector3(1f,1f,1f);
        }
        //going left
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
            //flips sprite to look left
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
        }

        //jumping 
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //GetComponent<AudioSource>().pla
            myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f); 
        }

        if (Input.GetButtonDown("Fire2"))
        {
            timeChange = true;
        }

      


        //animation setup
        myAnim.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
        myAnim.SetBool("Grounded", isGrounded);
    }

    //for kill plane
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Kill Plane")
        {
            //gameObject.SetActive(false);
            //transform.position = respawnPosition;
            theLevelManager.Respawn();
        }

        if(other.tag == "Checkpoint")
        {
            respawnPosition = other.transform.position;
        }
    }
    //used colision of objects
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Moving Platform")
        {
            transform.parent = collision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Moving Platform")
        {
            transform.parent = null;
        }
    }
}
