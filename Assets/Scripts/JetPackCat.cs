using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackCat : MonoBehaviour {

    private PlayerController thePlayer;

    public float chargeSpeed;

    public float hoverSpeed;

    public float playerRange;

    public LayerMask playerLayer;

    public bool playerInRange;

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        //Charge player when in range
        if (playerInRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, chargeSpeed * Time.deltaTime);
        }
        //Not in range, so random movements
        /*else
        {
            int moveXorY = Random.Range(0, 4);
            //If random is 1, then move X
            if (moveXorY != 1)
            {
                int moveUporDown = Random.Range(0, 2);
                if (moveUporDown == 1)
                {
                    // Move the object upward in world space 1 unit/second.
                    transform.Translate(Vector3.up * Time.deltaTime, Space.World);
                }
                else{ transform.Translate(Vector3.down * Time.deltaTime, Space.World); }
                
            }
            else
            {
                int moveLeftorRight = Random.Range(0, 2);
                if (moveLeftorRight == 1)
                {
                    // Move the object left in world space 1 unit/second.
                    transform.Translate(Vector3.left * Time.deltaTime, Space.World);
                }
                else { transform.Translate(Vector3.right * Time.deltaTime, Space.World); }
            }

        }*/
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawSphere(transform.position, playerRange);
    }

}
