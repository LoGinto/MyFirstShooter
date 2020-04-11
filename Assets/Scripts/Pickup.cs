using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    Inventory inventory;
    public GameObject itemButton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                for(int i = 0;i< inventory.slots.Length ; i++)
                {
                    if(inventory.isFull[i] == false)
                    {
                        //ADD ITEM
                        inventory.isFull[i] = true;
                        Instantiate(itemButton,inventory.slots[i].transform,false);
                        Destroy(gameObject);
                        break;

                    }
                }
            }
        }
    }
}
