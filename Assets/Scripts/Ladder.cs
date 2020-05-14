using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    FirstPersonMovement firstPersonMovement;
    public bool inside = false;
    GameObject player;
    public float heightFactor = 1.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        firstPersonMovement = player.GetComponent<FirstPersonMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            firstPersonMovement.enabled = false;
            inside = !inside;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
           firstPersonMovement.enabled = true;
           inside = !inside;
        }
    }

    void Update()
    {
        if (inside == true && Input.GetKey(KeyCode.W))
        {
            player.transform.position += Vector3.up / heightFactor;
        }
        if (inside == true && Input.GetKey(KeyCode.S))
        {
            player.transform.position += Vector3.down / heightFactor;
        }
    }
}
