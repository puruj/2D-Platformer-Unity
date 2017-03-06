using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnResspawn : MonoBehaviour {

    private Vector3 startPostion;
    private Quaternion startRotation;
    private Vector3 startLocalScale;

    private Rigidbody2D myRigedbody;

   
	// Use this for initialization
	void Start () {
        startPostion = transform.position;
        startRotation = transform.rotation;
        startLocalScale = transform.localScale;

        //if item does not have riged body do this
        if(GetComponent<Rigidbody2D>() != null)
        {
            myRigedbody = GetComponent<Rigidbody2D>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetObject()
    {
        transform.position = startPostion;
        transform.rotation = startRotation;
        transform.localScale = startLocalScale;

        if(myRigedbody != null)
        {
            myRigedbody.velocity = Vector3.zero;
        }
    }
}
