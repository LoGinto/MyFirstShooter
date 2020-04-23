using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMeat : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("I took" + gameObject+"s meat");//just a placeholder for now

        }
    }
}
