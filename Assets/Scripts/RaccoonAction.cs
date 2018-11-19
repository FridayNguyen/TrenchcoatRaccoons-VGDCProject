using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonAction : MonoBehaviour {

    //Unity defined variables
    public float jumpForce;
    public bool grounded;
    public LayerMask whatIsGround;

    private Collider2D myCollider;

    void Start () {
        myCollider = GetComponent<Collider2D>();
    }
	
	void Update () {
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
    }

    public void Jump()
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
    }
}
