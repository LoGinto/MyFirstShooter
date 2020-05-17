using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CommandAI : MonoBehaviour
{
    public GameObject myPartner = null;//I will set it empty for now
    public bool canControl = false;
    bool menuShowedUp;
    public Canvas canvas;
    public Text holdFireText;
    // Start is called before the first frame update
    void Start()
    {
        myPartner = GameObject.FindGameObjectWithTag("PlaceHolder");
        canControl = false;
    }

    // Update is called once per frame
    void Update()
    {
        NullifyControl();
        ShowOrHideMenu();
        AIBools();
        canvas.gameObject.SetActive(menuShowedUp);
        if (menuShowedUp)
        {
            holdFireText = GameObject.FindGameObjectWithTag("holdFireText").GetComponent<Text>();
        }
    }

    private void AIBools()
    {
        if (canControl && menuShowedUp && Input.GetKeyDown(KeyCode.F1))
        {
            myPartner.GetComponent<Partner>().SetFollowing(true);
            myPartner.GetComponent<Partner>().SetWaiting(false);
            menuShowedUp = false;
            Debug.Log(myPartner.name + " is Following");
        }
        if (canControl && menuShowedUp && Input.GetKeyDown(KeyCode.F2))
        {
            myPartner.GetComponent<Partner>().SetFollowing(false);
            myPartner.GetComponent<Partner>().SetWaiting(true);
            menuShowedUp = false;
            Debug.Log(myPartner.name + " is Waiting");
        }
        if (canControl && menuShowedUp && Input.GetKeyDown(KeyCode.F3))
        {
            if (myPartner.GetComponent<Partner>().GetAgressionState() == true)
            {
                myPartner.GetComponent<Partner>().SetAgressive(false);
                holdFireText.text = "f3.Hold Your Fire".ToString();
                menuShowedUp = false;
            }
            else
            {
                myPartner.GetComponent<Partner>().SetAgressive(true);
                holdFireText.text = "f3.Fire at will".ToString();
                menuShowedUp = false;
            }

        }
    }

    

    private void ShowOrHideMenu()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            menuShowedUp = !menuShowedUp;
        }
    }

    private void NullifyControl()
    {
        if (myPartner == null)
        {
            myPartner = GameObject.FindGameObjectWithTag("PlaceHolder");
            canControl = false;
        }
    }

    public void SetPartner(GameObject partner)
    {
        myPartner = partner;
    }
    public GameObject GetPartner()
    {
        if(myPartner.tag != "PlaceHolder" && canControl)
        {
            return myPartner;
        }
        else
        {
            return null;
        }
        
    }
}
