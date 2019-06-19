using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public InputField textName;
    public static string playerName;
    
    public GameObject PlayerName;


    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOverGO; //reference to the game over image
    public GameObject scoreUITextGO;
    public GameObject TimeCounterGO;

    public GameObject GameTitleGO;
    public GameObject PipelineSpawner;
    public GameObject ShootButton;

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    GameManagerState GMState;

    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
    }


    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:

                PlayerName.SetActive(true);

                //hide GameOver image
                GameOverGO.SetActive(false);

                //Display the game title
                GameTitleGO.SetActive(true);

                //Set play button
                playButton.SetActive(true);
                
                break;

            case GameManagerState.Gameplay:

                PlayerName.SetActive(false);
                //reset the score
                scoreUITextGO.GetComponent<GameScore>().Score = 0;

                //hide play button on game play state
                playButton.SetActive(false);
            
                //hide the game title
                GameTitleGO.SetActive(false);

                //display the button shoot
                ShootButton.SetActive(true);

                //set the player visible (active) and init the player lives
                playerShip.GetComponent<PlayerControl>().Init();

                //start enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

                //start pipeline spawner
                PipelineSpawner.GetComponent<PipelineSpawner>().SchedulePipelineSpawner();

                //start the time counter
                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();

                break;

            case GameManagerState.GameOver:

                PostToDataBase();
                //stop the time counter
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

                //hide button shoot
                ShootButton.SetActive(false);

                GameOverGO.SetActive(true);

                //stop enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();

                //stop pipeline spawner
                PipelineSpawner.GetComponent<PipelineSpawner>().UnschedulePipelineSpawner();

                //Change game manager state to Opening state after 8 sec
                Invoke("ChangeToOpeningState", 8f);

                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    //When the user click the play button
    public void StartGamePlay()
    {
        playerName = textName.text;
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    //When the user click input field
    public void DisplayKeyboard()
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true);
    }

    //Function to change Game manager state to opening state
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    private void PostToDataBase(){

        User user = new User();
        // using rest api to make application universal because SDK is only for mobile
        RestClient.Post("https://fire-base-projec.firebaseio.com/.json",user);
        RestClient.Post("https://my-space-shooter.firebaseio.com/.json",user);
        

    }
}
