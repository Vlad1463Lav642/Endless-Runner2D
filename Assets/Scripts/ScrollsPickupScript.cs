using UnityEngine;

/// <summary>
/// Обеспечивает подбор свитков.
/// </summary>
public class ScrollsPickupScript : MonoBehaviour
{
    [SerializeField] private Sprite toPlayerTransformation;
    [SerializeField] private RuntimeAnimatorController skeletonAnimator;

    private AudioSource scrollSound;

    private void Start()
    {
        scrollSound = GameObject.Find("BonusSound").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = toPlayerTransformation;
            collision.gameObject.GetComponentInChildren<Animator>().runtimeAnimatorController = skeletonAnimator;

            collision.gameObject.GetComponent<PlayerController>().SetTransformSkeleton(true);
            gameObject.SetActive(false);

            if (scrollSound.isPlaying)
            {
                scrollSound.Stop();
                scrollSound.Play();
            }
            else
            {
                scrollSound.Play();
            }
        }
    }
}