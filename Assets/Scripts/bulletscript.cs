using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
    public float speed = 3f;        //speed of bullet
    public float timer = 4f;        //time for bullet to despawn

    // Use this for initialization
    private void Awake()
    {
        Destroy(gameObject, timer);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}