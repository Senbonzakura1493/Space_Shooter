using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    Text LivesTextUI;

    public static int lives;

    public int Live
    {
        get
        {
            return lives;
        }
        set
        {
            lives = value;
            UpdateLivesTextUI();
        }
    }
    // Use this for initialization
    void Start()
    {
        //Get the Text UI componen of this gameObject
        LivesTextUI = GetComponent<Text>();
    }

    //Function to update the score text UI
    void UpdateLivesTextUI()
    {
        string liveStr = string.Format("{0:0}", Live);
        LivesTextUI.text = liveStr;
    }
}
