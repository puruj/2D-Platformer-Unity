using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    //how much time to wait before player respawns
    public float waitToRespawn;
    public PlayerController thePlayer;

    //explosion of player
    public GameObject deathExplosion;

    //keeps count of all coins in game
    public int coinCount;

    //reference to text box
    public Text coinText;

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
        //instantiate coins UI
        coinText.text = "Coins: " + coinCount;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Respawn()
    {
        StartCoroutine("RespawnCo");
    }

    // will make respawn timer it is co-routine
    public IEnumerator RespawnCo()
    {
        thePlayer.gameObject.SetActive(false);

        Instantiate(deathExplosion, thePlayer.transform.position, thePlayer.transform.rotation);
        //delay for respawn
        yield return new WaitForSeconds(waitToRespawn);
        thePlayer.transform.position = thePlayer.respawnPosition;
        thePlayer.gameObject.SetActive(true);
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        //updates coin count to UI
        coinText.text = "Coins: " + coinCount;
    }
}
