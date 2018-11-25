using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_detector : MonoBehaviour {

	// Use this for initialization
    public bool pickedup = false;
    public GameObject bullet;
    public float xIndex = 0f;       //Adjusts where the bullet spawns
    public float yIndex = 0f;

    void OnTriggerEnter2D(Collider2D coll)
    {
        // Sets pickedup to true and removes the pickup if a raccoon hits it.
        if(coll.gameObject.CompareTag("pickup"))
        {
            pickedup = true;
            print(pickedup);
            Destroy(coll.gameObject);
        }
    }

    public void Shoot()
    {
        if(pickedup)
        {
            Instantiate(bullet, new Vector3(transform.position.x + xIndex, transform.position.y + yIndex, 0f), Quaternion.identity);
        }
    }
}
