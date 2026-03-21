using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionArrow : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.gameObject.GetComponent<Collider>().isTrigger = true;
        }
    }
}
