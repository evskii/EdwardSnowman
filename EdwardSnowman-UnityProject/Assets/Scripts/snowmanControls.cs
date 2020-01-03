using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowmanControls : MonoBehaviour
{
    [Header("Move Left & Right")]
    public float moveSpeed;
    Rigidbody2D rb;
    public float moveSlide = 0.5f;

    [Header("Jump")]
    // the key used to activate the push
    public KeyCode keyJump = KeyCode.Space;
    // strength of the push
    public float jumpStrength = 10f;

    [Header("Ground setup")]
    //if the object collides with another object tagged as this, it can jump again
    public string groundTag = "Ground";
    //Check for ground, if false can't jump again
    public bool checkGround = true;
    private bool canJump = true;

    [Header("Animator")]
    public Animator BaseAnim;
    public Animator SnowmanAnim;

    void Start()
    {
        //Get Rigidbody for moving left and right
        rb = GetComponent<Rigidbody2D>();
    }

    // Read the input from the player
    void Update()
    {
        float moveDir = Input.GetAxis("Horizontal") * moveSpeed;

        if (moveDir == 0)
        {
            SnowmanAnim.SetBool("SnowmanIdle", true);
            BaseAnim.SetBool("BaseIdle", true);
            BaseAnim.SetBool("BaseLeft", false);
            BaseAnim.SetBool("BaseRight", false);
        }
        else if (moveDir != 0)
        {
            BaseAnim.SetBool("BaseIdle", false);
        }

        if (moveDir > 0)
        {
            BaseAnim.SetBool("BaseRight", true);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            rb.velocity = new Vector2(moveDir, rb.velocity.y);
        }
        else if(moveDir < 0)
        {
            BaseAnim.SetBool("BaseRight", true);
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            rb.velocity = new Vector2(moveDir, rb.velocity.y);
        }

        if (canJump && Input.GetKeyDown(keyJump))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            canJump = !checkGround;
            SnowmanAnim.SetBool("SnowmanJump", true);
            SnowmanAnim.SetBool("SnowmanIdle", false);

        }
        //If player moves and jumps
        if (moveDir > 0 && Input.GetKeyDown(keyJump) || moveDir < 0 && Input.GetKeyDown(keyJump) || moveDir == 0 && Input.GetKeyDown(keyJump))
        {
            SnowmanAnim.SetBool("SnowmanJump", true);
            SnowmanAnim.SetBool("SnowmanIdle", false);

        }
        
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y != 0) {
            SnowmanAnim.SetBool("SnowmanJump", true);
            SnowmanAnim.SetBool("SnowmanIdle", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collisionData)
    {
        if (checkGround
            && collisionData.gameObject.CompareTag(groundTag))
        {
            canJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.tag == "Enemy_Bullet") {
            SceneController.instnace.takeDamage(1);
            Destroy(collision);
        }
    }
}
