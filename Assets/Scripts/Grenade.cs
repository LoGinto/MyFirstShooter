using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 4f;
    float countDown;
    public float force;
    public float radius;
    [SerializeField] GameObject explosionVFX;
    bool hasExploded = false;
    private void Start()
    {
        countDown = delay;
    }
    private void Update()
    {
        countDown -= Time.deltaTime;
        if(countDown <= 0f && hasExploded==false)
        {
            Explode();
            hasExploded = true;
        }
    }
    void Explode()
    {
        Instantiate(explosionVFX, transform.position, transform.rotation);
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in collidersToDestroy)
        {             
            DestructibleObject dest = nearbyObject.GetComponent<DestructibleObject>();
            if(dest != null)
            {
                dest.ApplyDestruction();
            }
        }
        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

