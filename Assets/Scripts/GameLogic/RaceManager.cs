﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RaceManager : MonoBehaviour {

    private List<int> scoreByPlayerIndex;
    private RacingCheckpoint racingFor;
    private List<GameObject> playersThroughCheckpoint;

	// Use this for initialization
	void Start () {
        scoreByPlayerIndex = new List<int>();
        scoreByPlayerIndex.Add(0);
        scoreByPlayerIndex.Add(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void incrementPlayerScore(int playerIndex, int scoreIncrement)
    {
        while (scoreByPlayerIndex.Count <= playerIndex)
        {
            scoreByPlayerIndex.Add(0);
        }
        scoreByPlayerIndex[playerIndex] = scoreByPlayerIndex[playerIndex] + scoreIncrement;
    }

    public int getPlayerScore(int playerIndex)
    {
        if (playerIndex >= scoreByPlayerIndex.Count)
        {
            return 0;
        }
        return scoreByPlayerIndex[playerIndex];
    }

    public void hitCheckpoint(RacingCheckpoint checkPoint, GameObject player)
    {
        if (racingFor == null)
        {
            //Begin a race for the checkpoint
            racingFor = checkPoint;
            playersThroughCheckpoint = new List<GameObject>();
            playersThroughCheckpoint.Add(player);
        }
        else if (racingFor == checkPoint && !playersThroughCheckpoint.Contains(player))
        {
            //handle other players reaching the racing checkpoint
            playersThroughCheckpoint.Add(player);
            if (playersThroughCheckpoint.Count >= GameObject.FindGameObjectsWithTag("Car").Length)
            {
                //All players have reached the checkpoint in time. Reset it all and wait for the next checkpoint to be hit.
                racingFor = null;
            }
        }
    }
}
