using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnableInventory : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] bool openInventory = false;//test 
    [SerializeField] Transform weaponRoot;
    //bool cursorLocked = true;
    private void Start()
    {
        HideInventory();
    }

    // Update is called once per frame
    void Update()
    {
        EnableOrDisableInventory();

    }

    private void EnableOrDisableInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            openInventory = !openInventory;
                
          
            

        }
        if (openInventory == true)
        {
            
            ShowInventory();
            foreach (Transform child in weaponRoot)
            {
                child.gameObject.SetActive(false);//no problem in here
            }
            //Cursor.lockState = CursorLockMode.Confined;
            if (Cursor.lockState == CursorLockMode.Locked)
            {

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }
           
        }
        else
        {
           
            HideInventory();
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
        }
    }

    
    void HideInventory()
    {
        canvasGroup.alpha = 0f; 
        canvasGroup.blocksRaycasts = false; 
    }
    void ShowInventory()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    
}
