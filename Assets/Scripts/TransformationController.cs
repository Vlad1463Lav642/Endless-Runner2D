using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationController : MonoBehaviour
{
    [SerializeField] private Sprite toPlayerTransformation;
    [SerializeField] private RuntimeAnimatorController skeletonAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = toPlayerTransformation;
            collision.gameObject.GetComponentInChildren<Animator>().runtimeAnimatorController = skeletonAnimator;

            collision.gameObject.GetComponent<PlayerController>().SetTransformSkeleton(true);
            gameObject.SetActive(false);
        }
    }
}