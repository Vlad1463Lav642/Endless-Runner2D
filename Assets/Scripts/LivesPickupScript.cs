using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesPickupScript : MonoBehaviour
{
    private LiveManager liveManager;

    private void Start()
    {
        liveManager = FindObjectOfType<LiveManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            liveManager.PlusHeart();
            gameObject.SetActive(false);
        }
    }
}