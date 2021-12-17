using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private int coins;

    [SerializeField] private Text coinsCountText;   //prints score on screen

    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            collectionSoundEffect.Play();   //sound effect when coin selected
            Destroy(collision.gameObject);  // destroys coin object completely
            coins++;
            coinsCountText.text = "Coins : " + coins; //prints score on screen
        }
    }
}
