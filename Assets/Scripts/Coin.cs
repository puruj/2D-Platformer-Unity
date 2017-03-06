using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    //reference to level manager
    private LevelManager theLevelManager;

    //value of coins 
    public int coinValue;
	// Use this for initialization
	void Start () {
        //intantiate level manager
        theLevelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            theLevelManager.AddCoins(coinValue);

            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
