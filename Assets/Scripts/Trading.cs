using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChoicedDialogue
{
    public class Trading : MonoBehaviour
    {
        Inventory inventory;
        public GameObject itemButton;
        public int costTosale = 4;
        
        // Start is called before the first frame update
        void Start()
        {
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
            
        }

        // Update is called once per frame
        public void BuyItem()
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    //ADD ITEM
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    break;

                }
            }
        }//Inventory item
        public void SellItem()
        {
            //TO DO
        }
       //Also I should add bullets
    }
}