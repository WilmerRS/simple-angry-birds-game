using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    public float obtectResistence;
    public GameObject explosionPrefab;
    public AudioClip clip;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude <= obtectResistence)
        {
            obtectResistence -= collision.relativeVelocity.magnitude;
            return;
        }

        if(explosionPrefab != null)
        {
            var explosionAnimation = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosionAnimation, 0.1f);
        }
        
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, new Vector3(5, 1, 2));
        }

        Destroy(gameObject, 0.1f);
    }
}
