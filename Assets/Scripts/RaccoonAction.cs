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

    private void SpawnRaccoon()
    {
        PlayerController playerController = GameObject.Find("AllCoons-DoNotRename").GetComponent<PlayerController>();
        List<GameObject> allRaccoons = playerController.aliveRaccoonGameObjects;
        GameObject topRaccoon = allRaccoons[allRaccoons.Count - 1];

        print(topRaccoon.GetComponent<RaccoonAction>().coonIndex);

        Quaternion rotation = transform.rotation;
        Transform parent = GameObject.Find("AllCoons-DoNotRename").transform;
        Vector3 spawnPoint = topRaccoon.transform.GetChild(0).transform.position;

        allRaccoons.Add(Instantiate(this.gameObject, spawnPoint, rotation, parent));
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("enemy"))
        {
            GameObject.Find("AllCoons-DoNotRename").GetComponent<PlayerController>().aliveRaccoonGameObjects.Remove(gameObject);
            Instantiate(deathParticle, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        if (coll.gameObject.CompareTag("gunpickup"))
        {
            Destroy(coll.gameObject);
            hasGun = true;
        }
        if (coll.gameObject.CompareTag("raccoonpickup"))
        {
            Destroy(coll.gameObject);
//            SpawnRaccoon();
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("toptrigger"))
        {
            hasCoonBelow = true;
        }
        if (coll.gameObject.CompareTag("bottrigger"))
        {
            hasCoonAbove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("toptrigger"))
        {
            hasCoonBelow = false;
        }
        if (coll.gameObject.CompareTag("bottrigger"))
        {
            hasCoonAbove = false;
        }
    }
}
