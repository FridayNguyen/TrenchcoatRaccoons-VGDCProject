using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_detector : MonoBehaviour {

	// Use this for initialization
    private bool pickedup = false;

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
}
