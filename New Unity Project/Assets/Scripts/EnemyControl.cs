using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    GameObject scoreUITextGO; // reference tot he text UI game object

    public GameObject ExplosionGO; //explosion prefab

    public float speed;

    // Initialization
    void Start()
    {
        speed = 2f;

        //Get the score text UI
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        //Get the enemy current position
        Vector2 position = transform.position;

        //Compute the enemy new position
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        //Update the enemy position
        transform.position = position;

        //this is the botton-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //If the enemy went outside the screen on the bottom, then destroy the enemy
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    // collision
    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect collision of the enemy ship with the player ship, or with a player's bullet
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();

            //add 100 points to the score
            scoreUITextGO.GetComponent<GameScore>().Score += 100;

            Destroy(gameObject); // Destroy this enemy ship
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
