using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOverGO; //reference to the game over image
    public GameObject scoreUITextGO;
    public GameObject TimeCounterGO;
    public GameObject GameTitleGO;

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

                //hide GameOver image
                GameOverGO.SetActive(false);

                //Display the game title
                GameTitleGO.SetActive(true);

                //Set play button
                playButton.SetActive(true);
                
                break;

            case GameManagerState.Gameplay:

                //reset the score
                scoreUITextGO.GetComponent<GameScore>().Score = 0;

                //hide play button on game play state
                playButton.SetActive(false);

                //hide the game title
                GameTitleGO.SetActive(false);

                //set the player visible (active) and init the player lives
                playerShip.GetComponent<PlayerControl>().Init();

                //start enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

                //start the time counter
                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();

                break;

            case GameManagerState.GameOver:

                //stop the time counter
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

                GameOverGO.SetActive(true);

                //stop enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();

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
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    //Function to change Game manager state to opening state
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
