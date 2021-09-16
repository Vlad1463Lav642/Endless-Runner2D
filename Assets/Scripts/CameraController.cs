using UnityEngine;

/// <summary>
/// Обеспечивает управление камерой.
/// </summary>
public class CameraController : MonoBehaviour
{
    private PlayerController player;

    private Vector3 lastPosition;
    private float distanceX;
    private float distanceY;

    [SerializeField] private Transform catcher; //Смертельная область под платформами.

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        lastPosition = player.transform.position;
    }

    private void Update()
    {
        distanceX = player.transform.position.x - lastPosition.x;
        distanceY = player.transform.position.y - lastPosition.y;

        transform.position = new Vector3(transform.position.x + distanceX, transform.position.y + distanceY, transform.position.z); //Перемещение камеры.
        catcher.position = new Vector3(catcher.position.x, catcher.position.y - distanceY, catcher.position.z); //Сдвиг смертельной области по Y, если игрок поднялся выше или спустился.

        lastPosition = player.transform.position;
    }
}