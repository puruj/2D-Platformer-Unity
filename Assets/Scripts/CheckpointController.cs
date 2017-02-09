using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {


    public Sprite torchClosed;
    public Sprite torchOpen;

    private SpriteRenderer theSpriteRenderer;

    public bool checkPointActive;

	// Use this for initialization
	void Start () {
        theSpriteRenderer = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            theSpriteRenderer.sprite = torchOpen;
            checkPointActive = true;
        }
    }
}
