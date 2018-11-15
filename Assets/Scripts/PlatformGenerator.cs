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
            float distanceBetween = Random.Range(0, 2);
            
            float floatingPlatform = Random.Range(0, 3);

            transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween, transform.position.y, transform.position.z);
            Instantiate(thePlatform, transform.position, transform.rotation);

            if (floatingPlatform != 0)
            {
                int randomHeight = Random.Range(2, 8);
                Vector3 tempPosition = new Vector3(transform.position.x + theFloatingPlatform.GetComponent<BoxCollider2D>().size.x, transform.position.y + randomHeight, transform.position.z);
                Instantiate(theFloatingPlatform, tempPosition, transform.rotation);
            }
        }
    }
}
