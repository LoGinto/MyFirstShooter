using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterACar : MonoBehaviour
{
    [SerializeField]bool entered;
    CarDriving carDriving;
    GameObject player;
    [SerializeField] float entranceDistance;
    public Camera carCam;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        carDriving = GetComponent<CarDriving>();
        entered = false;
        
    }
    private void Update()
    {
        carDriving.enabled = entered;
        carCam.gameObject.SetActive(entered);
        player.SetActive(!entered);
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (isOnSelectedDistanceToPlayer(entranceDistance))
            {
                entered = !entered;
            }
           
        }
        if (entered)
        {
            player.transform.parent = gameObject.transform;
            
        }
        else
        {
            
            player.transform.parent = null;
        }

       
    }
    private bool isOnSelectedDistanceToPlayer(float distance)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= distance;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, entranceDistance);
    }
}


