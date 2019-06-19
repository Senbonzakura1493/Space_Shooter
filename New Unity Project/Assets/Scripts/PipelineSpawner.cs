using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineSpawner : MonoBehaviour
{

    public GameObject Pipeline; //Pipeline prefab

    float SpawnRate = 10f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPipeline()
    {
        //Bottom-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //Top-Right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //Instanciate an Pipeline
        GameObject PipelineInstance = (GameObject)Instantiate(Pipeline);
        PipelineInstance.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        //Schedule to spawn next Pipeline
        ScheduleNextPipelineSpawn();
    }

    // handles the next spawn when timer reached
    void ScheduleNextPipelineSpawn()
    {

        Invoke("SpawnPipeline", SpawnRate); //appel la fonction après spawnRate
    }

    //Function to start Pipeline spawner
    public void SchedulePipelineSpawner()
    {
        Invoke("SpawnPipeline", SpawnRate);  // an Pipeline appear après SpawnRate

    }

    //Function to stop Pipeline spawner
    public void UnschedulePipelineSpawner()
    {
        CancelInvoke("SpawnPipeline");
    }


}
