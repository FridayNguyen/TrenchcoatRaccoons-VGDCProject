using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

    public GameObject thePlatform;
    public GameObject theFloatingPlatform;
    public Transform generationPoint;

    private float platformWidth;

    // Use this for initialization
    void Start()
    {
        platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {   
            //Variables
            float distanceBetweenChance = Random.Range(0, 4);
            float floatingPlatform = Random.Range(0, 2);
            int distanceBetween = 0;

            //One out of four chance will there be a hole in the floor
            if (distanceBetweenChance == 0)
            {
                distanceBetween = 1;
            }

            //Floor Platform, distancebetween will vary due to random distance chance
            transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween, transform.position.y, transform.position.z);
            Instantiate(thePlatform, transform.position, transform.rotation);

            //Random floating platforms 2/3 chance
            if (floatingPlatform != 0)
            {
                int randomHeight = Random.Range(3, 8);
                int randomWidth = Random.Range(2,4);
                Vector3 tempPosition = new Vector3(transform.position.x + theFloatingPlatform.GetComponent<BoxCollider2D>().size.x + randomWidth, transform.position.y + randomHeight, transform.position.z);
                Instantiate(theFloatingPlatform, tempPosition, transform.rotation);
            }
        }
    }
}
