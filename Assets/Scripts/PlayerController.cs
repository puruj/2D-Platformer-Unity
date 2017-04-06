using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    private float activeMoveSpeed;
    public float jumpSpeed;

    public bool canMove;

    //ground check
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;

    //Time change
    public bool timeChange; 

    public Rigidbody2D myRigidbody;

    //Animation
    private Animator myAnim;
    public Vector3 respawnPosition;

    //reference to Level Manager
    public LevelManager theLevelManager;

    //reference to box that kills enemies
    public GameObject stompBox;

    //pushing back player
    public float knockBackForce;
    public float knockBackLength;
    private float knockBackCounter;
    public float invincibiltyLength;
    private float invincibiltyCounter;
    //sounds
    public AudioSource jumpSound;
    public AudioSource hurtSound;

    private bool onPlatform;
    public float onPlatformSpeedModifier;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        //sets initial respawn point
        respawnPosition = transform.position;
        //initializing level manager
        theLevelManager = FindObjectOfType<LevelManager>();

        activeMoveSpeed = moveSpeed;
        canMove = true;
	}
	
	// Update is called once per frame
	void Update () {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,whatIsGround);

        if (knockBackCounter <= 0  && canMove ==true)
        {

            if (onPlatform)
            {
                activeMoveSpeed = moveSpeed * onPlatformSpeedModifier;
            }
            else
            {
                activeMoveSpeed = moveSpeed;
            }


            //moving left and right
            //going right
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                myRigidbody.velocity = new Vector3(activeMoveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            //going left
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                myRigidbody.velocity = new Vector3(-activeMoveSpeed, myRigidbody.velocity.y, 0f);
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
                jumpSound.Play();
            }

            
        }
      
        if(knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;
            //going left to right
            if (transform.localScale.x > 0)
            {
                myRigidbody.velocity = new Vector3(-knockBackForce, knockBackForce, 0f);
            }
            //going right to left
            else {
                myRigidbody.velocity = new Vector3(knockBackForce, knockBackForce, 0f);
            }
        }
        //time of invincibilty
        if (invincibiltyCounter > 0)
        {
            invincibiltyCounter -= Time.deltaTime;
        }

        if (invincibiltyCounter <= 0)
        {
            theLevelManager.invincible = false;
        }

        //animation setup
        myAnim.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
        myAnim.SetBool("Grounded", isGrounded);

        if (myRigidbody.velocity.y < 0)
        {
            stompBox.SetActive(true);
        }
        else
        {
            stompBox.SetActive(false);
        }
    }

    public void knockBack()
    {
        knockBackCounter = knockBackLength;
        invincibiltyCounter = invincibiltyLength;
        theLevelManager.invincible = true;
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
            onPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Moving Platform")
        {
            transform.parent = null;
            onPlatform = false;
        }
    }
}
