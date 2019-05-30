using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject GameManagerGO;

    public GameObject PlayerBulletGO; // player's bullet prefab
    public GameObject BulletPosition1;
    public GameObject BulletPosition2;
    public GameObject ExplosionGO; //explosion prefab

    public Text LivesUIText;

    const int MaxLives = 3;
    int lives; //current player lives

    public float speed;

    // on object initiation setup
    public void Init()
    {
        lives = MaxLives;

        //update the lives UI Text
        LivesUIText.text = lives.ToString();

        //Reset the player position to the center of the screen (when the user clicks on the play button)
        transform.position = new Vector2(0, 0);

        //set this player game object to active
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // fire bullets when the spacebar is pressed
        if (Input.GetKeyDown("space"))
        {
            //play the laser sound effect
            GetComponent<AudioSource>().Play();

            //instanciation bullet 1
            GameObject bullet1 = (GameObject)Instantiate(PlayerBulletGO);
            bullet1.transform.position = BulletPosition1.transform.position; // set the bullet initial position

            //instanciation bullet 2
            GameObject bullet2 = (GameObject)Instantiate(PlayerBulletGO);
            bullet2.transform.position = BulletPosition2.transform.position; // set the bullet initial position
        }


        float x = Input.GetAxisRaw("Horizontal");  // -1, 0, 1 --- left,no input,right
        float y = Input.GetAxisRaw("Vertical"); // -1, 0, 1 --- down, no input, up

        // based on the input, we calculate a direction vector, and normalize it to have a unit vector
        Vector2 direction = new Vector2(x, y).normalized;

        //function that changes the player's position 
        Move(direction);

    }

    void Move(Vector2 direction)
    {
        //Find the screen limits to the player's movement
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //corner bottom-left of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //Take into account the player
        max.x = max.x - 0.225f; // substract the player sprite half width
        min.x = min.x + 0.225f; // add  the player sprite half width

        max.y = max.y - 0.225f; // substract the player sprite half height
        min.y = min.y + 0.225f; // add the player sprite half height

        //Gives Player's current position
        Vector2 pos = transform.position;

        //Calculates new position
        pos += direction * speed * Time.deltaTime;

        //to make sure that the new position is not outside the screen
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        //update position of the player
        transform.position = pos;
    }


    // Detect collision of the player ship with an enemy ship, or with an enemy bullet
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();

            lives--; //substract one live
            LivesUIText.text = lives.ToString(); //update lives UI text

            if (lives == 0)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                gameObject.SetActive(false);
            }
        }
    }

    //To instanciate an explosion
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        //set the position of the explosion
        explosion.transform.position = transform.position;
    }



}
