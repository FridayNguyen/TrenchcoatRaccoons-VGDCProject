using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonAction : MonoBehaviour {

    //Unity defined variables
    public float jumpForce;
    public bool grounded;
    public LayerMask whatIsGround;

    public GameObject deathParticle;

    private Animator myAnimator;

    public AudioSource ShootSound;
    public AudioSource JumpSound;
    public AudioSource Raccon_DeathSound;

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
    public GameObject bullet;

    private PlayerController playerController;

    void Start () {
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
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
        isCurrentCoon = (playerController.selectedIndex == coonIndex);

        this.transform.Find("gunhand").GetComponent<Renderer>().enabled = hasGun;
       

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
        JumpSound.Play();
    }

    public void Shoot()
    {
        if (hasGun)
        {
            Vector3 bulletSpawn = this.transform.GetChild(3).transform.position;
            Instantiate(bullet, bulletSpawn, Quaternion.identity);
            ShootSound.Play();
            hasGun = false;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        switch(coll.gameObject.tag)
        {
            case "enemy":
                Destroy(gameObject);
                Instantiate(deathParticle, gameObject.transform.position, gameObject.transform.rotation);
                Raccon_DeathSound.Play();
                break;

            case "gunpickup":
                Destroy(coll.gameObject);
                hasGun = true;
                break;

            case "raccoonpickup":
                Destroy(coll.gameObject);
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        hasCoonBelow = coll.gameObject.CompareTag("toptrigger");
        hasCoonAbove = coll.gameObject.CompareTag("bottrigger");
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        hasCoonBelow = !coll.gameObject.CompareTag("toptrigger");
        hasCoonAbove = !coll.gameObject.CompareTag("bottrigger");
    }

    void OnDestroy()
    {
        List<GameObject> allRaccoons = playerController.aliveRaccoonGameObjects;
        int index = allRaccoons.IndexOf(gameObject);
        if (index == allRaccoons.Count - 1)
            playerController.SelectDown();
        allRaccoons.Remove(gameObject);
        if (allRaccoons.Count == 0) playerController.Restart();
    }
}
