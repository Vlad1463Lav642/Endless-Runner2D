using UnityEngine;

/// <summary>
/// Скрипт обеспечивает подбор сокровищ.
/// </summary>
public class CoinsPickupScript : MonoBehaviour
{
    [SerializeField] private int scoreToGive;

    private ScoreManager scoreManager;

    private AudioSource coinSound;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        coinSound = GameObject.Find("BonusSound").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            scoreManager.AddScore(scoreToGive);
            gameObject.SetActive(false);

            if (coinSound.isPlaying)
            {
                coinSound.Stop();
                coinSound.Play();
            }
            else
            {
                coinSound.Play();
            }
        }
    }

}