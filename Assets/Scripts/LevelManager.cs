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
    private int internalCoinCount;
    public int amtNeededForLife;
    public AudioSource coinPickUp;

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

    //making player invincible on knockback
    public bool invincible;

    //lives
    public Text liveText;
    public int startingLives;
    public int currentLives;

    //game over screen
    public GameObject gameOverScreen;

    //game music
    public AudioSource levelMusic;
    public AudioSource levelMusic2;
    public AudioSource gameOverMusic;
    public AudioSource endLevelMusic;

    public bool respawnCoActive;

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
        //instantiate coins UI
        coinText.text = "Coins: " + coinCount;
        //instantiate health
        healthCount = maxHealth;

        objectsToReset = FindObjectsOfType<ResetOnResspawn>();
        if (PlayerPrefs.HasKey("coinCount"))
        {
            coinCount = PlayerPrefs.GetInt("coinCount");
        }
        
        //instantiate coins UI
        coinText.text = "Coins: " + coinCount;

        currentLives = startingLives;
        liveText.text = "Lives x " + currentLives;
    }
	
	// Update is called once per frame
	void Update () {
		if(healthCount <= 0 && !respawning)
        {
            Respawn();
            respawning = true;
        }

        if(internalCoinCount >= amtNeededForLife)
        {
            currentLives += 1;
            liveText.text = "Lives x " + currentLives;
            internalCoinCount -= amtNeededForLife;
        }
	}

    public void Respawn()
    {
        currentLives -= 1;
        liveText.text = "Lives x " + currentLives;

        if (currentLives > 0)
        {
            StartCoroutine("RespawnCo");
        }

        else
        {
            //take away player control from user
            thePlayer.gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
            levelMusic.Stop();
            levelMusic2.Stop();
            gameOverMusic.Play();
        }
    }

    // will make respawn timer it is co-routine
    public IEnumerator RespawnCo()
    {
        respawnCoActive = true;

        //take away player control from user
        thePlayer.gameObject.SetActive(false);

        Instantiate(deathExplosion, thePlayer.transform.position, thePlayer.transform.rotation);
        //delay for respawn
        yield return new WaitForSeconds(waitToRespawn);

        respawnCoActive = false;

        healthCount = maxHealth;
        respawning = false;
        updateHeartMeter();
        //sets coins to zero after death
        coinCount = 0;
        internalCoinCount = 0;
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
        internalCoinCount += coinsToAdd;
        //updates coin count to UI
        coinText.text = "Coins: " + coinCount;
        coinPickUp.Play();
    }

    //does damage to player
    public void HurtPlayer(int dmg)
    {
        if (!invincible)
        {
            healthCount -= dmg;
            updateHeartMeter();
            //call to knockback function
            thePlayer.knockBack();
            //hurt sound
            thePlayer.hurtSound.Play();
        }
    }

    public void GiveHealth(int healthToGive)
    {
        healthCount += healthToGive;
        if(healthCount > maxHealth)
        {
            healthCount = maxHealth;
        }
        coinPickUp.Play();

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

    public void AddLives(int livesToAdd)
    {
        coinPickUp.Play();
        currentLives += livesToAdd;
        liveText.text = "Lives x " + currentLives;
    }
}
