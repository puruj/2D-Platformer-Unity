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

    //reference to heart images
    public Image heart1;
    public Image heart2;
    public Image heart3;

    //sprites of differnt heart types
    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;
    
    //health stats
    public int maxHealth;
    public int healthCount;

    private bool respawning;

    public ResetOnResspawn[] objectsToReset;
    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
        //instantiate coins UI
        coinText.text = "Coins: " + coinCount;
        //instantiate health
        healthCount = maxHealth;

        objectsToReset = FindObjectsOfType<ResetOnResspawn>();
    }
	
	// Update is called once per frame
	void Update () {
		if(healthCount <= 0 && !respawning)
        {
            Respawn();
            respawning = true;
        }
	}

    public void Respawn()
    {
        StartCoroutine("RespawnCo");
    }

    // will make respawn timer it is co-routine
    public IEnumerator RespawnCo()
    {
        //take away player control from user
        thePlayer.gameObject.SetActive(false);

        Instantiate(deathExplosion, thePlayer.transform.position, thePlayer.transform.rotation);
        //delay for respawn
        yield return new WaitForSeconds(waitToRespawn);

        healthCount = maxHealth;
        respawning = false;
        updateHeartMeter();
        //sets coins to zero after death
        coinCount = 0;
        coinText.text = "Coins: " + coinCount;


        //respawn player
        thePlayer.transform.position = thePlayer.respawnPosition;
        //give player control to user
        thePlayer.gameObject.SetActive(true);

        for(int i = 0; i < objectsToReset.Length; i++)
        {
            objectsToReset[i].gameObject.SetActive(true);
            objectsToReset[i].ResetObject();
        }
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        //updates coin count to UI
        coinText.text = "Coins: " + coinCount;
    }

    //does damage to player
    public void HurtPlayer(int dmg)
    {
        healthCount -= dmg;
        updateHeartMeter();
    }

    public void updateHeartMeter()
    {
        switch (healthCount)
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                return;
            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                return;
            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                return;
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                return;
            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
             case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
        }


    }
}
