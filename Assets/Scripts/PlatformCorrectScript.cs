using UnityEngine;

/// <summary>
/// Корректирует спавн платформ и убирает наложившиеся друг на друга платформы.
/// </summary>
public class PlatformCorrectScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            collision.gameObject.SetActive(false);
        }
    }
}