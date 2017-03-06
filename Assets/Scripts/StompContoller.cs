using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompContoller : MonoBehaviour {

    public GameObject deathExplosion;

    private Rigidbody2D playerRigedbody;

    public float bounceForce;

	// Use this for initialization
	void Start () {
        playerRigedbody = transform.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
            Instantiate(deathExplosion, collision.transform.position, collision.transform.rotation);

            playerRigedbody.velocity = new Vector3(playerRigedbody.velocity.x,bounceForce, 0f);
        }
    }
}
