using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public GameObject destroyedGameObject;

    public void ApplyDestruction()
    {
        Instantiate(destroyedGameObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
