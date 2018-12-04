using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhitdetector : MonoBehaviour
{
    private AudioSource Enemy_DeathSound;
    public GameObject deathParticle;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))      //Checks if the trigger is a bullet
        {
            Destroy(collision.gameObject);
            BulletDetected();
            CheckWinningCondition();
        }
    }

    //This method runs when a bullet collides with this object.
    void BulletDetected()
    {
        Instantiate(deathParticle, gameObject.transform.position, gameObject.transform.rotation);
        Enemy_DeathSound = GameObject.Find("Enemy_DeathSound").GetComponent<AudioSource>();
        Enemy_DeathSound.Play();
        Destroy(this.gameObject);
    }

    void CheckWinningCondition()
    {
        if(gameObject.name == "sun")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().LoadGameWon();
        }
    }
}
