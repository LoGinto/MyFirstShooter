using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandAI : MonoBehaviour
{
    public GameObject myPartner = null;//I will set it empty for now
    public bool canControl = false;
    bool menuShowedUp;
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            menuShowedUp = !menuShowedUp;
        }
        if (canControl&&menuShowedUp&&Input.GetKeyDown(KeyCode.Alpha1))
        {
            myPartner.GetComponent<Partner>().SetFollowing(true);
            myPartner.GetComponent<Partner>().SetWaiting(false);
            Debug.Log(myPartner.name + " is Following");
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
    
}
