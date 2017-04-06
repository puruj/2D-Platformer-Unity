using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

    public string levelToLoad;
    public string levelToUnlock;

    private PlayerController thePlayer;
    private CameraController theCamera;
    private LevelManager theLevelManager;

    public float waitToMove;
    public float waitToLoad;

    private bool movePlayer;

    public Sprite flagOpen;
    private SpriteRenderer theSpriteRenderer;

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
        theCamera = FindObjectOfType<CameraController>();
        theLevelManager = FindObjectOfType<LevelManager>();

        theSpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (movePlayer)
        {
            thePlayer.myRigidbody.velocity = new Vector3(thePlayer.moveSpeed, thePlayer.myRigidbody.velocity.y, 0f);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            theSpriteRenderer.sprite = flagOpen;
            //SceneManager.LoadScene(levelToLoad);
            StartCoroutine("LevelEndCo");
        }
    }
     
    public IEnumerator LevelEndCo()
    {
        thePlayer.canMove = false;
        theCamera.followTarget = false;
        theLevelManager.invincible = true;

        theLevelManager.levelMusic.Stop();
        theLevelManager.endLevelMusic.Play();

        PlayerPrefs.SetInt("CoinCount", theLevelManager.coinCount);
        PlayerPrefs.SetInt(levelToUnlock, 1);

        thePlayer.myRigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(waitToMove);

        movePlayer = true;

        //loads next lvl
        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(levelToLoad);
    }

}
