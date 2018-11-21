using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour {

    [SerializeField]
    public float moveSpeed;

    [SerializeField]
    public float frequency;

    [SerializeField]
    public float magnitude;

    Vector3 pos, localScale;

    // Use this for initialization
    void Start()
    {
        pos = transform.position;
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeft();
    }

    void MoveLeft()
    {
        pos -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }
}
