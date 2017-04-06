using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour {

    public string levelToLoad;
    public bool unLocked;

    public Sprite doorBottomOpen;
    public Sprite doorTopOpen;
    public Sprite doorBottomClosed;
    public Sprite doorTopClosed;

    public SpriteRenderer doorTop;
    public SpriteRenderer doorBottom;

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("Level1", 1);

        if(PlayerPrefs.GetInt(levelToLoad) == 1)
        {
            unLocked = true;
        }
        else
        {
            unLocked = false;
        }


        if (unLocked)
        {
            doorTop.sprite = doorTopOpen;
            doorBottom.sprite = doorBottomOpen;
        }
        else
        {
            doorTop.sprite = doorTopClosed;
            doorBottom.sprite = doorBottomClosed;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (Input.GetButtonDown("Jump") && unLocked)
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }

}
