using System.Collections;
using System.Collections.Generic; //For Queue
using UnityEngine;

public class TeachersController : MonoBehaviour
{
    public GameObject Combefis; //Prefab
    public GameObject Lurkin;
    public GameObject VDD;
    public int maxTeachers;

    

    // Start is called before the first frame update
    void Start()
    {
        //bottom left-point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //top right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //loop to create stars
        for (int i = 0; i < maxTeachers; i++)
        {
            GameObject T_Combefis = (GameObject)Instantiate(Combefis);
            GameObject T_Lurkin = (GameObject)Instantiate(Lurkin);
            GameObject T_VDD = (GameObject)Instantiate(VDD);

            //set the position of the star
            T_Combefis.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            T_Lurkin.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            T_VDD.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

            //set a random speed for the star
            T_Combefis.GetComponent<Teachers>().speed = -(1f * Random.value + 0.5f);
            T_Lurkin.GetComponent<Teachers>().speed = -(1f * Random.value + 0.5f);
            T_VDD.GetComponent<Teachers>().speed = -(1f * Random.value + 0.5f);

            //make the star a child of the starGeneratorGO
            T_Combefis.transform.parent = transform;
            T_Lurkin.transform.parent = transform;
            T_VDD.transform.parent = transform;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

}
