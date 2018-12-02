using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhitdetector : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))      //Checks if the trigger is a bullet
        {
            Destroy(collision.gameObject);
            BulletDetected();
        }

    }

    //This method runs when a bullet collides with this object.
    void BulletDetected()
    {
        Destroy(this.gameObject);
    }
}
