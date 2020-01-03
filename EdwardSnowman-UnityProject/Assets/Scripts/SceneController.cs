using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instnace;

    [Header("Sun Changing Options:")]
    public float sunTimeLength;
    public Image sunImage;
    private bool sunsOut;

    [Header("Damage Options:")]
    public bool inCover;
    public int playerHealth;
    public float damageTick;
    private float lastDamage = 0;

    [Header("UI Options:")]
    public Text healthText;

    [Header("PickUp:")]
    private int itemsPickedUp;
    public GameObject HatUI, ScarfUI, CoalUI;
    public GameObject Hat, Scarf, Coal;//Object to add to player
    public bool endReady;


    private void Awake() {
        instnace = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        sunsOut = true;
        StartCoroutine(sunChange());
        sunImage = sunImage.GetComponent<Image>();
        sunImage.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if(playerHealth <= 0) {
            SceneManager.LoadScene("GameTesting");
        }

        if (itemsPickedUp >= 3) {
            endReady = true;
        }

        //UI Controls
        healthText.text = "Health: " + playerHealth;

        if (sunsOut) {
            if (!inCover) {//If the sun is out and the player isnt in cover
                if(lastDamage + damageTick <= Time.time) {
                    lastDamage = Time.time;
                    takeDamage(1);
                }
            }
        } 
        else {
            
        }
        
    }

    public void takeDamage(int damage) {
        Debug.Log("Taking Damage");
        playerHealth -= damage;
    }

    IEnumerator sunChange() {
        yield return new WaitForSeconds(sunTimeLength);
        changeDayState();
        StartCoroutine(sunChange());
    }

    void changeDayState() {
        if (sunsOut) {
            Debug.Log("Sun's in.");
            sunsOut = false;
            sunImage.color = new Color(0,0,0,0.75f);
        } else {
            Debug.Log("Sun's Out!");
            sunsOut = true;
            sunImage.color = new Color(0, 0, 0, 0);
        }
    }

    public void pickedUpItem(string item)
    {
        itemsPickedUp++;
        if(item == "Hat")
        {
            HatUI.SetActive(true);
            Hat.SetActive(true);
        }
        if (item == "Scarf")
        {
            ScarfUI.SetActive(true);
            Scarf.SetActive(true);
        }
        if (item == "Coal")
        {
            CoalUI.SetActive(true);
            Coal.SetActive(true);
        }
    }
}
