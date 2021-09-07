using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController player;

    private Vector3 lastPosition;
    private float distanceX;
    private float distanceY;

    [SerializeField] private Transform catcher;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        lastPosition = player.transform.position;
    }

    private void Update()
    {
        distanceX = player.transform.position.x - lastPosition.x;
        distanceY = player.transform.position.y - lastPosition.y;

        transform.position = new Vector3(transform.position.x + distanceX, transform.position.y + distanceY, transform.position.z);
        catcher.position = new Vector3(catcher.position.x, catcher.position.y - distanceY, catcher.position.z);

        lastPosition = player.transform.position;
    }
}