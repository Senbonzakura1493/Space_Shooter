using System.Collections;
using System.Collections.Generic; //For Queue
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public GameObject[] Planets;

    //Queue to hold the planets
    Queue<GameObject> availablePlanets = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //Add the planets to the queue
        availablePlanets.Enqueue(Planets[0]);
        availablePlanets.Enqueue(Planets[1]);
        availablePlanets.Enqueue(Planets[2]);

        //cal the MovePlanetDown function every 20 seconds
        InvokeRepeating("MovePlanetDown", 0, 20f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //To dequeue a planet, and set its isMoving flag to true
    //so that the planet starts scrolling down the screen
    void MovePlanetDown()
    {
        EnqueuePlanets();

        //if the queue is empty
        if (availablePlanets.Count == 0)
            return;

        //get a planet from the queue
        GameObject aPlanet = availablePlanets.Dequeue();

        //set the planet isMoving flag to true
        aPlanet.GetComponent<Planet>().isMoving = true;
    }

    //To Enqueue planets that are below the screen and are not moving
    void EnqueuePlanets()
    {
        foreach (GameObject aPlanet in Planets)
        {
            //if the planet is below the screen, and the planet is not moving
            if ((aPlanet.transform.position.y < 0) && (!aPlanet.GetComponent<Planet>().isMoving))
            {
                //reset the planet position
                aPlanet.GetComponent<Planet>().ResetPosition();

                //enqueue the planet
                availablePlanets.Enqueue(aPlanet);
            }
        }
    }
}
