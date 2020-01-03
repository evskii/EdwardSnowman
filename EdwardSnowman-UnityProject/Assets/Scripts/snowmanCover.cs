using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowmanCover : MonoBehaviour
{
    public float rayLength;
    public GameObject coverCheck;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(coverCheck.transform.position, Vector2.up, rayLength);
        //Debug.DrawRay(coverCheck.transform.position, new Vector3(0,1,0), Color.yellow, rayLength);
        if (hit) {
            SceneController.instnace.inCover = true;
        } else {
            SceneController.instnace.inCover = false;
        }
    }
}
