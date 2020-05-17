using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public float throwForce;
    [SerializeField] GameObject grenade;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject grenadeObj =  Instantiate(grenade, transform.position, transform.rotation);
            Rigidbody rb = grenadeObj.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwForce,ForceMode.VelocityChange);
        }
    }
    
}
