using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public GameObject Hat, Scarf, Coal;
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag.Equals("Hat"))
        {
            SceneController.instnace.pickedUpItem("Hat");
            Destroy(Hat);
        }

        if (c.gameObject.tag.Equals("Scarf"))
        {
            SceneController.instnace.pickedUpItem("Scarf");
            Destroy(Scarf);
        }

        if (c.gameObject.tag.Equals("Coal"))
        {
            SceneController.instnace.pickedUpItem("Coal");
            Destroy(Coal);
        }
    }
}
