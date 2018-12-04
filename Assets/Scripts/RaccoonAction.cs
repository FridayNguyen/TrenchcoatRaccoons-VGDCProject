using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaccoonAction : MonoBehaviour {

    //Unity defined variables
    public float jumpForce;
    public bool grounded;
    public LayerMask whatIsGround;

    public GameObject deathParticle;

    private Animator myAnimator;

    private AudioSource ShootSound;
    private AudioSource JumpSound;
    private AudioSource Raccoon_DeathSound;

    public bool isTop = true;
    public bool isBottom = true;
    public bool isMiddle = false;
    public bool isJumpingUp = false;
    public bool isFallingDown = false;
    public bool hasGun = false;
    public bool hasCoonAbove = false;
    public bool hasCoonBelow = false;
    public bool isCurrentCoon = false;
    public int coonIndex;
    private Collider2D myCollider;
    public GameObject bullet;
  
    private PlayerController playerController;
    private bool racpickedup = false;

    void Start () {       
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();

        JumpSound = GameObject.Find("JumpSound").GetComponent<AudioSource>();
        ShootSound = GameObject.Find("ShootSound").GetComponent<AudioSource>();
        Raccoon_DeathSound = GameObject.Find("Raccoon_DeathSound").GetComponent<AudioSource>();
    }
	
	void Update () {
        //grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        grounded = myCollider.IsTouchingLayers(whatIsGround);
        coonIndex = playerController.aliveRaccoonGameObjects.IndexOf(gameObject);

        isJumpingUp = (GetComponent<Rigidbody2D>().velocity.y > 0);
        isFallingDown = (GetComponent<Rigidbody2D>().velocity.y < 0);
        isTop = !hasCoonAbove;
        isBottom = !hasCoonBelow;
        isMiddle = (!isTop && !isBottom);
        isCurrentCoon = (playerController.selectedIndex == coonIndex);

        this.transform.Find("gunhand").GetComponent<Renderer>().enabled = hasGun;
        this.transform.Find("SelectIndicator").GetComponent<Renderer>().enabled = isCurrentCoon;


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
        Rigidbody2D myrigidbody2D = GetComponent<Rigidbody2D>();
        myrigidbody2D.velocity = new Vector2(myrigidbody2D.velocity.x, jumpForce);
        JumpSound.Play();
    }

    public void Shoot()
    {
        if (hasGun)
        {
            Vector3 bulletSpawn = this.transform.GetChild(3).transform.position;
            Instantiate(bullet, bulletSpawn, Quaternion.identity);
            hasGun = false;
            ShootSound.Play();           
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "toptrigger" && !coll.transform.IsChildOf(transform))
        {
            hasCoonBelow = true;
        }       
        if (coll.gameObject.tag == "bottrigger" && !coll.transform.IsChildOf(transform))
        {
            hasCoonAbove = true;
        }
        if (coll.gameObject.tag == "raccoonpickup" && !racpickedup)
        {
            coll.GetComponent<raccoonAdd>().SpawnRaccoon();
            racpickedup = true;
        }
        switch (coll.gameObject.tag)
        {
            case "enemy":
                WhenDestroy();
                Destroy(gameObject);              
                break;

            case "gunpickup":
                Destroy(coll.gameObject);
                hasGun = true;
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {

    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (racpickedup)
        {
            racpickedup = false;
        }
        if (coll.gameObject.name == "TopTrigger" && !coll.transform.IsChildOf(transform))
        {
            hasCoonBelow = false;
        }
        if (coll.gameObject.name == "BotTrigger" && !coll.transform.IsChildOf(transform))
        {
           hasCoonAbove = false;
        }
    }

    public void WhenDestroy()
    {
        if (hasCoonBelow)
        {
            playerController.aliveRaccoonGameObjects[coonIndex - 1].GetComponent<RaccoonAction>().hasCoonAbove = false;
        }
        if (hasCoonAbove)
        {
            playerController.aliveRaccoonGameObjects[coonIndex + 1].GetComponent<RaccoonAction>().hasCoonBelow = false;
        }
        Instantiate(deathParticle, gameObject.transform.position, gameObject.transform.rotation);       
        List<GameObject> allRaccoons = playerController.aliveRaccoonGameObjects;
        int index = allRaccoons.IndexOf(gameObject);

        if (index == allRaccoons.Count - 1)
            playerController.SelectDown();

        allRaccoons.Remove(gameObject);       

        if (allRaccoons.Count == 0)
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        Raccoon_DeathSound.Play();
    }

    void OnBecameInvisible()
    {
        WhenDestroy();
        Destroy(gameObject);
    }
}
