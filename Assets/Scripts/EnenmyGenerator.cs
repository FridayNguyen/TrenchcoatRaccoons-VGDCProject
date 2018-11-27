using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenmyGenerator : MonoBehaviour {

    public GameObject jetPackCat;
    public GameObject bird;

    public void SpawnEnenmy(Vector3 startPosition, string enemy)
    {
 
        // 50% chance a bird will spawn or 50% chance a cat will spawn
        if (enemy == "bird")
        {
            Instantiate(bird, startPosition, transform.rotation);
        }
        else
        {
            Instantiate(jetPackCat, startPosition, transform.rotation);
        }
    }
}
