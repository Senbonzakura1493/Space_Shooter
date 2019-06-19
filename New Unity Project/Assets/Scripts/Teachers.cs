using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teachers : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Get the current position of the Teacher
        Vector2 position = transform.position;
        
        //Compute the Teacher's new position
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        //Update the Teacher's position
        transform.position = position;

        //this is the bottom-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //top-right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //if the Teacher goes outside the screen on the bottom,
        //then position the Teacher on the top edge of the screen
        //and randomly between the left and right side of the screen
        if (transform.position.y < min.y -2)
        {
            transform.position = new Vector2(Random.Range(min.x, max.x), max.y + 2); // +2 to don't see the disappearing effect
        }
    }
}
