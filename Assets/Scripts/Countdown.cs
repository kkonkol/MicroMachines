﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {
	public GameObject countdownField;
	public GameObject countdownFieldShadow;
        
	public float delay = 0.5f;
	string message;
	Text textcomp;
	Text textcompShadow;

    AudioSource cdSfx;

	// Use this for initialization
	void Start () {
        //lock players
        GameObject p1 = GameObject.Find("Player1");    
        GameObject p2 = GameObject.Find("Player2");

        p1.GetComponent<Driving>().canDrive = false;
        p2.GetComponent<Driving>().canDrive = false;
        p1.GetComponent<DrivingSound>().engineOn = true;
        p2.GetComponent<DrivingSound>().engineOn = true;

        cdSfx = GetComponent<AudioSource>();

		//countdown zeug
		textcomp = countdownField.GetComponent<UnityEngine.UI.Text>();
		textcomp.text = "";
		textcompShadow = countdownFieldShadow.GetComponent<UnityEngine.UI.Text>();
		textcompShadow.text = "";

		message = "321!";
		StartCoroutine (TypeText () );
	}

	IEnumerator TypeText(){
		foreach (char letter in message.ToCharArray()) {
			textcomp.text = "" + letter;
			textcompShadow.text = "" + letter;

            cdSfx.Play();
			yield return new WaitForSeconds (delay);
		}
        //unlock players
        GameObject p1 = GameObject.Find("Player1");
        GameObject p2 = GameObject.Find("Player2");

        p1.GetComponent<Driving>().canDrive = true;
        p2.GetComponent<Driving>().canDrive = true;
		countdownField.SetActive (false);
		countdownFieldShadow.SetActive (false);
	}
		
}
