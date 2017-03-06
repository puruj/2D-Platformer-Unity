using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour {

    public float moveSpeed;
    private bool canMove;

    private Rigidbody2D myRigedbody;

	// Use this for initialization
	void Start () {
        myRigedbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (canMove)
        {
            myRigedbody.velocity = new Vector3(-moveSpeed, myRigedbody.velocity.y, 0f);
        }
	}

    void OnBecameVisible()
    {
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "KillPlane")
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
    //stops spider from moving after respawn
    private void OnEnable()
    {
        canMove = false;
    }
}
