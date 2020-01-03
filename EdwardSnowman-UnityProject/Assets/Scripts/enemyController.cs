using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [Header("Enemy Setup Options: ")]
    GameObject player;

    public float attackDistance;
    private float distanceFromPlayer;

    private bool patrolling =true;
    private float startPosition;
    public float patrollingDistance;
    public float moveDirection = 1;
    public float moveSpeed;
    private float enemySpeed;

    [Header("Shooting")]
    public Rigidbody2D bullet;
    public float bulletSpeed = 5;
    public GameObject muzzleLeft;
    private bool movingLeft;
    public GameObject muzzleRight;
    public float damageTick;
    private float lastDamage = 0;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPosition = gameObject.transform.position.x;
        enemySpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector2.Distance(gameObject.transform.position, player.transform.position);
        Debug.Log("Distance from player: " + distanceFromPlayer);

        if(distanceFromPlayer > attackDistance) {
            patrolling = true;
            enemySpeed = moveSpeed;
        } else {
            patrolling = false;
            enemySpeed = 0;
        }

        if (patrolling) {
            Debug.Log("Patrolling");
            //move the enemy left until he hits a point then move right
            if(gameObject.transform.position.x >= startPosition + patrollingDistance) {
                moveDirection = -1;
                movingLeft = true;
            } else if(gameObject.transform.position.x <= startPosition - patrollingDistance) {
                moveDirection = 1;
                movingLeft = false;
            }

            transform.Translate(new Vector3(moveDirection, 0, 0) * moveSpeed);
        } else {
            //Attack player
            if (lastDamage + damageTick <= Time.time) {
                lastDamage = Time.time;
                shoot();
            }
        }
        
    }

    void shoot() {
        if (movingLeft) {
            Rigidbody2D snowball; //Create a new Rigidbody variable.
            snowball = Instantiate(bullet, muzzleLeft.transform.position, Quaternion.identity); //Set the variables value to an instantiated bullet
            snowball.transform.parent = null; //Remove the parent of the bullet so it drops by itself
            snowball.velocity = transform.TransformDirection(new Vector3(-bulletSpeed, 0, 0)); //Add velocity to the bullet so that it goes down at an angle.
        } else {
            Rigidbody2D snowball; //Create a new Rigidbody variable.
            snowball = Instantiate(bullet, muzzleRight.transform.position, Quaternion.identity); //Set the variables value to an instantiated bullet
            snowball.transform.parent = null; //Remove the parent of the bullet so it drops by itself
            snowball.velocity = transform.TransformDirection(new Vector3(bulletSpeed, 0, 0)); //Add velocity to the bullet so that it goes down at an angle.
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.tag == "Player_Bullet") {
            Destroy(gameObject);
            Destroy(collision);
        }
    }
}
