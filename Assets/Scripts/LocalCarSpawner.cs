﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocalCarSpawner : MonoBehaviour {

    private List<GameObject> players;

	// Use this for initialization
    void Start()
    {
        GameObject choiceManager = GameObject.Find("ChoiceManager");
        if (choiceManager == null)
        {
            Debug.Log("NO CHOICE MANAGER");
            Destroy(gameObject);
            return;
        }
        players = new List<GameObject>();
        List<GameObject> picks = choiceManager.GetComponent<LocalChoiceManager>().getPicks();
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawnpunkt");
        int i = -1;
        foreach (GameObject pick in picks)
        {
            i++;
            GameObject spawn = Instantiate(pick);
            spawn.transform.position = spawnPoints[i % spawnPoints.Length].transform.position;
            spawn.GetComponent<Driving>().enabled = true;
            spawn.name = "Player" + (i+1);
            players.Add(spawn);
        }
        Destroy(choiceManager);
	}

    public GameObject getPlayerByNumber(int number)
    {
        return players[number];
    }

    public GameObject getPlayer1()
    {
        return players[0];
    }

    public GameObject getPlayer2()
    {
        return players[1];
    }
	
	// Update is called once per frame
    void Update()
    {
	}
}
