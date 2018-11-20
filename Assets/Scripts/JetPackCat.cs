using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackCat : MonoBehaviour
{

    private PlayerController thePlayer;

    private Vector3 originalPosition;

    public float chargeSpeed;

    public float hoverSpeed;

    public float hoverVerticalLength;

    public float playerRange;

    public LayerMask playerLayer;

    public bool playerInRange;

    public bool isCharging;

    private bool hoverUp;

    // Use this for initialization
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        originalPosition = transform.position;
        isCharging = false;
        hoverUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        //Charge player when in range
        if (playerInRange)
        {
            isCharging = true;
            transform.Translate(Vector2.left * chargeSpeed * Time.deltaTime);
        }
        //Not in range, so random movements
        else
        {
            if ((transform.position.y < (originalPosition.y + hoverVerticalLength)) && hoverUp)
            {
                transform.Translate(Vector2.up * Time.deltaTime);
            }
            else
            {
                hoverUp = false;
                transform.Translate(Vector2.down * Time.deltaTime);
                if (transform.position.y < (originalPosition.y - hoverVerticalLength))
                {
                    hoverUp = true;
                }
            }
        }
        isCharging = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, playerRange);
    }

}
