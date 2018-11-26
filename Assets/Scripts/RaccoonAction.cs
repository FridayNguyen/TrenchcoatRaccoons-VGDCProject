using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonAction : MonoBehaviour {

    //Unity defined variables
    public float jumpForce;
    public bool grounded;
    public LayerMask whatIsGround;

    private Animator myAnimator;

    public bool isTop = false;
    public bool isBottom = false;
    public bool isMiddle = false;
    public bool isJumpingUp = false;
    public bool isFallingDown = false;
    public bool hasGun = false;
    public bool hasCoonAbove = false;
    public bool hasCoonBelow = false;
    public bool isCurrentCoon = false;
    public int coonIndex;
    private Collider2D myCollider;

    void Start () {
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
    }
	
	void Update () {
        //grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        grounded = myCollider.IsTouchingLayers(whatIsGround);

        isJumpingUp = (GetComponent<Rigidbody2D>().velocity.y > 0);
        isFallingDown = (GetComponent<Rigidbody2D>().velocity.y < 0);
        isTop = !hasCoonAbove;
        isBottom = !hasCoonBelow;
        isMiddle = (!isTop && !isBottom);
        isCurrentCoon = (GameObject.Find("AllCoons-DoNotRename").GetComponent<PlayerController>().selectedIndex == coonIndex);

        myAnimator.SetBool("grounded", grounded);
        myAnimator.SetBool("isTop", isTop);
        myAnimator.SetBool("isBottom", isBottom);
        myAnimator.SetBool("isMiddle", isMiddle);
        myAnimator.SetBool("isJumpingUp", isJumpingUp);
        myAnimator.SetBool("isFallingDown", isFallingDown);
        myAnimator.SetBool("hasGun", hasGun);
        myAnimator.SetBool("hasCoonAbove", hasCoonAbove);
        myAnimator.SetBool("hasCoonBelow", hasCoonBelow);
        myAnimator.SetBool("isCurrentCoon", isCurrentCoon);
    }

    public void Jump()
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        
        if (coll.gameObject.CompareTag("enemy"))
        {
            GameObject.Find("AllCoons-DoNotRename").GetComponent<PlayerController>().aliveRaccoonGameObjects.Remove(gameObject);
            Destroy(gameObject);
        }
        else if (coll.gameObject.CompareTag("toptrigger"))
        {
            hasCoonBelow = true;
        }
        else if (coll.gameObject.CompareTag("bottrigger"))
        {
            hasCoonAbove = true;
        }
        else if (coll.gameObject.CompareTag("gunpickup"))
        {
            hasGun = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("toptrigger"))
        {
            hasCoonBelow = false;
        }
        else if (coll.gameObject.CompareTag("bottrigger"))
        {
            hasCoonAbove = false;
        }
    }
}
