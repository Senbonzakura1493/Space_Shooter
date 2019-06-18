﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScore : MonoBehaviour
{

    Text scoreTextUI;

    public static int score;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            UpdateScoreTextUI();
        }
    }
    // Use this for initialization
    void Start()
    {
        //Get the Text UI componen of this gameObject
        scoreTextUI = GetComponent<Text>();
    }

    //Function to update the score text UI
    void UpdateScoreTextUI()
    {
        string scoreStr = string.Format("{0:0000000}", score);
        scoreTextUI.text = scoreStr;
    }
}
