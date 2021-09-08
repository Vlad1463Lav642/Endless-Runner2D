using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    private GameObject platformDestroyerPoint;

    private void Start()
    {
        platformDestroyerPoint = GameObject.FindGameObjectWithTag("Destroyer");
    }

    private void Update()
    {
        if(transform.position.x < platformDestroyerPoint.transform.position.x)
        {
            gameObject.SetActive(false);
        }
    }
}