using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raccoonAdd : MonoBehaviour {

    public GameObject raccoon;
    private GameObject allCoons;


    public void SpawnRaccoon()
    {
        PlayerController playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        List<GameObject> allRaccoons = playerController.aliveRaccoonGameObjects;
        GameObject topRaccoon = allRaccoons[allRaccoons.Count - 1];

        Quaternion rotation = transform.rotation;
        Transform parent = GameObject.Find("AllCoons-DoNotRename").transform;
        Vector3 spawnPoint = topRaccoon.transform.GetChild(0).transform.position;

        allRaccoons.Add(Instantiate(raccoon, spawnPoint, rotation, parent));

        Destroy(gameObject);
    }
}
