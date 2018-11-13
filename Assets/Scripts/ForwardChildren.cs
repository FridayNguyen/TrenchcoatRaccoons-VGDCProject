using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardChildren : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    public float moveSpeed;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        moveSpeed = this.transform.root.GetComponent<ForwardMaster>().moveSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
	}
}
