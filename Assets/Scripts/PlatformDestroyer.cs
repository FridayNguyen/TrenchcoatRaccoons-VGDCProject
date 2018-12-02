using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour {

    public GameObject platformDestructionPoint;
    public GameObject deathParticle;

	// Use this for initialization
	void Start () {
        platformDestructionPoint = GameObject.Find("PlatformDestructionPoint");
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < platformDestructionPoint.transform.position.x || transform.position.y < platformDestructionPoint.transform.position.y)
        {
            if (gameObject.CompareTag("Player"))
            {
                GameObject.Find("AllCoons-DoNotRename").GetComponent<PlayerController>().aliveRaccoonGameObjects.Remove(gameObject);
                Instantiate(deathParticle, gameObject.transform.position, gameObject.transform.rotation);
            }
            Destroy(gameObject);
        }
	}
}
