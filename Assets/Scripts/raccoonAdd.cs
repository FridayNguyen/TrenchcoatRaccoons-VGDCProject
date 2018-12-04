using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raccoonAdd : MonoBehaviour {

    public void SpawnRaccoon()
    {
        PlayerController playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        playerController.SpawnRaccoon();



        Destroy(gameObject);
    }
}
