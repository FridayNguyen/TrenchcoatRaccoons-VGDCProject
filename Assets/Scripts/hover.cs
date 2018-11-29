using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hover : MonoBehaviour {

    public float rate = 1f;             //how often it goes up and down
    public float speed = 1f;            //how fast it goes up and down
    public float destroytime = 40f;     //time until item despawns
    private float timer = .1f;

    private void Awake()
    {
        Destroy(gameObject, destroytime);
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (timer > 0 && timer < rate)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            timer += Time.deltaTime;
        }
        else if (timer > 0)
        {
            timer = -.1f;
        }
        else if (timer < 0 && timer > -rate)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
            timer -= Time.deltaTime;
        }
        else if (timer < 0)
        {
            timer = .1f;
        }
    }
}
