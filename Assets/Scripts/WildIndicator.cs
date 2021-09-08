using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WildIndicator : MonoBehaviour
{
    [SerializeField] private Sprite arrowLeft;
    [SerializeField] private Sprite arrowRight;

    [SerializeField] GameObject arrowObject;
    [SerializeField] private float wildTimer;
    private float wildTimerCount;

    private PlayerController player;

    [SerializeField] private float wildRandomNumber;

    private bool wildRotation;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        wildTimerCount = wildTimer;

        wildRotation = true;

        if(Random.Range(0f, 10f) > wildRandomNumber)
        {
            WildRotator();
        }
    }

    private void Update()
    {
        if(wildTimerCount > 0)
        {
            wildTimerCount -= Time.deltaTime;
        }
        else
        {
            WildRotator();
            wildTimerCount = wildTimer;
        }
    }

    public void WildRotator()
    {
        wildRotation = !wildRotation;

        if (wildRotation)
        {
            arrowObject.GetComponent<Image>().sprite = arrowRight;
            player.RunWild();
        }
        else
        {
            arrowObject.GetComponent<Image>().sprite = arrowLeft;
            player.RunWild();
        }
    }

    public bool GetWildRotate()
    {
        return wildRotation;
    }
}