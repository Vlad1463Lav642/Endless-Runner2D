using UnityEngine;

/// <summary>
/// Скрывает платформы оставшиеся позади игрока.
/// </summary>
public class PlatformDestroyer : MonoBehaviour
{
    private GameObject platformDestroyerPoint;
    private PlayerController player;

    private void Start()
    {
        platformDestroyerPoint = GameObject.FindGameObjectWithTag("Destroyer");
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(transform.position.x < platformDestroyerPoint.transform.position.x && player.GetIsGround())
        {
            gameObject.SetActive(false);
        }
    }
}