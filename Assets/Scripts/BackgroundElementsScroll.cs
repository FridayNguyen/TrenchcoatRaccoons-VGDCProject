using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundElementsScroll : MonoBehaviour
{

    public float scrollspeed = 2f;

    void FixedUpdate()
    {
        // Moves right at a certain speed
        transform.Translate(Vector3.right * Time.deltaTime * scrollspeed);
    }

    void OnBecameInvisible()
    {
        Instantiate(gameObject, transform.position + new Vector3(38.2f,0f,0f), transform.rotation);
    }
}