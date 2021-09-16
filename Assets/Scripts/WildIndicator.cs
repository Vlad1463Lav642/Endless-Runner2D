using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Обеспечивает управление индикатором направления ветра.
/// </summary>
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

    /// <summary>
    /// Меняет направление ветра.
    /// </summary>
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

    /// <summary>
    /// Возвращает текущее направление ветра.
    /// </summary>
    /// <returns>Направление ветра.</returns>
    public bool GetWildRotate()
    {
        return wildRotation;
    }
}