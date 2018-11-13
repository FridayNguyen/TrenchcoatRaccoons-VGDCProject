using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Unity defined variables
    public float jumpForce;
    public bool grounded;
    public LayerMask whatIsGround;

    private static GameObject selectedRacoonGameObject;
    private Collider2D myCollider;
    
    void Start () {
        selectedRacoonGameObject = RacoonSelector.Select(0);
        print(selectedRacoonGameObject);
        myCollider = GetComponent<Collider2D>();
	}
	
	void Update () {
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        ListenControls();
	}
    
    private void ListenControls()
    {
        if(Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Rigidbody2D rigidbody2D = selectedRacoonGameObject.GetComponent<Rigidbody2D>();
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce*RacoonSelector.GetTopStack());
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedRacoonGameObject = RacoonSelector.SelectUp();
                print(selectedRacoonGameObject);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedRacoonGameObject = RacoonSelector.SelectDown();
                print(selectedRacoonGameObject);
            }
        }
    }
}
