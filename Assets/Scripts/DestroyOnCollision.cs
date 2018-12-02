using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour {

    public GameObject col;

    /*void Start()
    {
        
    }

    void Update()
    {
        OnCollisionEnter2D(Collision2D col);
    }*/

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(col.gameObject);
        }
    }
}
