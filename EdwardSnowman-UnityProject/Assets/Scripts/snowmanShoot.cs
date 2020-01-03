using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class snowmanShoot : MonoBehaviour
{
    [Header("Object creation")]

    public Rigidbody2D bullet;
    public KeyCode keyShoot = KeyCode.K;
    public float snowballSpeed = 5;
    public GameObject muzzle;
    public float destroyTime = 2;

    void Start()
    {
        Destroy(bullet, destroyTime);
    }
    void Update()
    {
        if (Input.GetKeyUp(keyShoot))
        {
            Rigidbody2D snowball; //Create a new Rigidbody variable.
            snowball = Instantiate(bullet, muzzle.transform.position, Quaternion.identity); //Set the variables value to an instantiated bullet
            snowball.transform.parent = null; //Remove the parent of the bullet so it drops by itself
            snowball.velocity = transform.TransformDirection(new Vector3(snowballSpeed,0,0)); //Add velocity to the bullet so that it goes down at an angle.
        }
    }
}