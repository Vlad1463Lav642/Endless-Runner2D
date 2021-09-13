using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesPickupScript : MonoBehaviour
{
    private LiveManager liveManager;

    private AudioSource heartSound;

    private void Start()
    {
        liveManager = FindObjectOfType<LiveManager>();
        heartSound = GameObject.Find("BonusSound").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            liveManager.PlusHeart();
            gameObject.SetActive(false);

            if (heartSound.isPlaying)
            {
                heartSound.Stop();
                heartSound.Play();
            }
            else
            {
                heartSound.Play();
            }
        }
    }
}