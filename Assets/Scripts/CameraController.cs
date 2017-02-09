using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    //will be player
    public GameObject target;
    //leading camera value
    public float followAhead;
    public float smoothing;

    private Vector3 targetPosition;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        targetPosition = new Vector3(target.transform.position.x,transform.position.y,transform.position.z);
        // facing right
        if(target.transform.localScale.x > 0f)
        {
            targetPosition = new Vector3(targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
        }
        //facing left
        else
        {
            targetPosition = new Vector3(targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
        }
        //transform.position = targetPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition,smoothing*Time.deltaTime);

         
    }
}
