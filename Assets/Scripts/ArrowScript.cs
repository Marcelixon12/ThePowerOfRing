using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasHit = false;

    // Wybierz oœ, która ma byæ grotem (domyœlnie w Unity to Vector3.forward)
    // Jeœli strza³a leci bokiem, spróbuj zmieniæ to na Vector3.up lub Vector3.right
    [SerializeField] private Vector3 forwardDirection = Vector3.forward;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!hasHit && rb.velocity.sqrMagnitude > 0.5f)
        {
            // Tworzymy rotacjê skierowan¹ w stronê ruchu
            Quaternion targetRotation = Quaternion.LookRotation(rb.velocity);

            // Korygujemy rotacjê modelu, jeœli jego oœ "przodu" jest inna ni¿ Z
            transform.rotation = targetRotation * Quaternion.FromToRotation(forwardDirection, Vector3.forward);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        hasHit = true;
        rb.isKinematic = true;
        transform.parent = collision.transform;
        if (collision.gameObject.CompareTag("Skeleton"))
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Skeleton>().ChangeHealth(CharacterMovement.currentDamage);
        }
    }
}
