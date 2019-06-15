using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    Text timeUI;

    float startTime;//the time when the user click on play
    float ellapsedTime; //the ellapsed time after the user clicks on play
    bool startCounter;//flag to start the counter

    int minutes;
    int seconds;

    // Start is called before the first frame update
    void Start()
    {
        startCounter = false;

        //get the Text UI component from this gameObject
        timeUI = GetComponent<Text>();
        
    }

    //Function to start the time counter
    public void StartTimeCounter()
    {
        startTime = Time.time;
        startCounter = true;
    }


    //Function to stop the time counter
    public void StopTimeCounter()
    {
        startCounter = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(startCounter)
        {
            //compute the ellapsed time
            ellapsedTime = Time.time - startTime;

            minutes = (int)ellapsedTime / 60;
            seconds = (int)ellapsedTime % 60;

            //update the time counter UI Text
            timeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        
    }
}
