using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float aliveTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, aliveTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
    }
}
