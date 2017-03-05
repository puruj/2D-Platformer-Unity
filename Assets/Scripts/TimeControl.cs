using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour {

    bool currentTime = false;
    GameObject[] past, present;
    bool triggered = false;

    public AudioClip[] audios;

   // public GameObject bg;
    //public Material pa, pr;

	// Use this for initialization
	void Start () {
        past = GameObject.FindGameObjectsWithTag("Past");
        present = GameObject.FindGameObjectsWithTag("Present");
        for (int i = 0; i < present.Length; i++)
            present[i].SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.T))
            triggered = true;

        if (triggered)
        {
            if (currentTime)
            {
                //bg.GetComponent<Renderer>().material = pr;
                for(int i = 0; i < past.Length; i++)
                {
                    past[i].SetActive(true);
                }
                for (int i = 0; i < present.Length; i++)
                {
                    present[i].SetActive(false);
                }
            }else
            {
                //bg.GetComponent<Renderer>().material = pa;
                for (int i = 0; i < past.Length; i++)
                {
                    past[i].SetActive(false);
                }
                for (int i = 0; i < present.Length; i++)
                {
                    present[i].SetActive(true);
                }
            }
            triggered = false;
            currentTime = !currentTime;
        }
	}
}
