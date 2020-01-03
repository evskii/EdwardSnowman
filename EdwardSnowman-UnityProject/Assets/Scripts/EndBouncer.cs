using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBouncer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) {
        if (SceneController.instnace.endReady) {
            SceneManager.LoadScene("Winner");
        }
    }
}
