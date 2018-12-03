using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raccoonAdd : MonoBehaviour {

    public GameObject raccoon;
    private GameObject allCoons;

	// Adds a raccoon to the index when raccoonpickup is destroyed
	void OnDestroy()
    {
        allCoons = GameObject.Find("AllCoons-DoNotRename");
        if(allCoons != null)
        {
            PlayerController playerController = allCoons.GetComponent<PlayerController>();
            List<GameObject> allRaccoons = playerController.aliveRaccoonGameObjects;
            GameObject topRaccoon = allRaccoons[allRaccoons.Count - 1];

            print(topRaccoon.GetComponent<RaccoonAction>().coonIndex);

            Quaternion rotation = transform.rotation;
            Transform parent = GameObject.Find("AllCoons-DoNotRename").transform;
            Vector3 spawnPoint = topRaccoon.transform.GetChild(0).transform.position;

            allRaccoons.Add(Instantiate(raccoon, spawnPoint, rotation, parent));
        }
    }
}
