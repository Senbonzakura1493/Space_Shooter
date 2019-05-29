using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyGO; //Enemy prefab

    float maxSpawnRateInSeconds = 5f;

    // Initialization
    void Start()
    {
        Invoke("SpawnEnemy", maxSpawnRateInSeconds); // an enemy appear après maxSpawnRateInSeconds

        //Increase spawn rate every 30 sec
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Function to spawn enemy (at the top of the screen, randomly)
    void SpawnEnemy()
    {
        //Bottom-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //Top-Right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //Instanciate an enemy
        GameObject EnemyInstance = (GameObject)Instantiate(EnemyGO);
        EnemyInstance.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        //Schedule to spawn next enemy
        ScheduleNextEnemySpawn();
    }

    // handles the next spawn when timer reached
    void ScheduleNextEnemySpawn()
    {
        float spawnInSeconds;

        if (maxSpawnRateInSeconds > 1f)
        {
            //a number between 1 and maxSpawnRateInSeconds
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
            spawnInSeconds = 1f;

        Invoke("SpawnEnemy", spawnInSeconds); //appel la fonction après spawnInSeconds
    }

    //To increase difficulty
    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;

        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }
}
