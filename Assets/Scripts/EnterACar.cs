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
    private bool enteredAI;
    CommandAI commandAI;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        carDriving = GetComponent<CarDriving>();
        entered = false;
        commandAI = player.GetComponent<CommandAI>();
    }
    private void Update()
    {
        carDriving.enabled = entered;
        carCam.gameObject.SetActive(entered);
        player.SetActive(!entered);
        if (commandAI.myPartner.tag != "PlaceHolder"&&commandAI.canControl == true)
        {
            commandAI.myPartner.SetActive(!entered);
        }
            
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
            if (commandAI.GetPartner() != null)
            {
                if(commandAI.myPartner.GetComponent<Partner>().GetWaiting() == false)
                {
                    commandAI.myPartner.transform.parent = gameObject.transform;
                    enteredAI = true;
                }
            }
            
        }
        else
        {
            
            player.transform.parent = null;
            if (commandAI.GetPartner() != null&&enteredAI == true)
            {
                commandAI.transform.parent = null;
                enteredAI = false;
            }
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


