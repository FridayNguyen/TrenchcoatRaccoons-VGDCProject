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
        GameObject mainCamera = GameObject.Find("Main Camera");

        Quaternion rotation = transform.rotation;
        Transform parent = GameObject.Find("AllCoons-DoNotRename").transform;
        float x = topRaccoon.transform.GetChild(0).transform.position.x;
        float y = mainCamera.transform.position.y + mainCamera.transform.localScale.y;
        Vector3 spawnPoint = new Vector3(x, y); 

        allRaccoons.Add(Instantiate(raccoon, spawnPoint, rotation, parent));

        Destroy(gameObject);
    }
}
