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

    private bool wildRotation;

    private void Start()
    {
        wildRotation = true;
        player = FindObjectOfType<PlayerController>();
        wildTimerCount = wildTimer;
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