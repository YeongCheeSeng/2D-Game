using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public int score;
    //public AudioSource pickUpSound;
    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //pickUpSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Coin Collected");

            //if (pickUpSound != null)
            //{
            //    Debug.Log("Play " + pickUpSound);
            //    pickUpSound.Play();
            //}


            player.TotalScore += score;
            Destroy(this.gameObject);
        }
    }
}
