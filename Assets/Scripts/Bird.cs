using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Brid : MonoBehaviour
{
    public Transform slingshotPivotPoint;
    public GameObject explosionPrefab;
    public float springRange;
    public float maximumSpeed;

    Rigidbody2D birdRigidbody2D;
    bool canDragTheBird = true;

    Vector3 distanceToPivot;

    [SerializeField] private AudioSource fliyinAudioSource;

    void Start() {
        birdRigidbody2D = GetComponent<Rigidbody2D>();
        birdRigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnMouseDrag() {
        if(!canDragTheBird)
        {
            return;
        }

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distanceToPivot = mousePosition - slingshotPivotPoint.position;

        // to ignore z index
        distanceToPivot.z = 0;

        if(distanceToPivot.magnitude > springRange)
        {
            distanceToPivot = distanceToPivot.normalized * springRange;
        }

        transform.position = distanceToPivot + slingshotPivotPoint.position;
    }

    void OnMouseUp()
    {
        if (!canDragTheBird)
        {
            return;
        }

        canDragTheBird = false;

        birdRigidbody2D.bodyType = RigidbodyType2D.Dynamic;

        var linearRelationBetweenBirdAndRange = distanceToPivot.magnitude / springRange;
        fliyinAudioSource.Play();
        birdRigidbody2D.velocity = -distanceToPivot.normalized * maximumSpeed * linearRelationBetweenBirdAndRange;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (explosionPrefab != null)
        {
            var explosionAnimation = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosionAnimation, 0.1f);
        }
    }
}
