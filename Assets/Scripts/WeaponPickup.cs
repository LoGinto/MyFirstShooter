using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] GameObject weaponToInstantiate;
    [SerializeField] int indexToReplace;
    [SerializeField] GameObject weaponRoot;
    [SerializeField] Transform instantiateObjectAt;

    Quaternion instance;
    Transform insta;
    private void Update()
    {
        instance = instantiateObjectAt.rotation;
        insta = instantiateObjectAt.transform;
        if (instantiateObjectAt == null || insta == null)
        {
            try {
                int i = 0;
                foreach (Transform child in weaponRoot.transform)
                {
                    if (i == indexToReplace)
                    {
                        insta = child.transform;
                        break;
                    }
                    i++;
                }
            }
            catch 
            {
                
                foreach (Transform child in weaponRoot.transform)
                {
                    insta = child.transform;
                    break;
                }
            }


    }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            int i = 0;
            foreach (Transform child in weaponRoot.transform)
            {
                if (i == indexToReplace)
                {
                    //Vector3 childPos = child.transform.position;
                    Destroy(child.gameObject);
                    Instantiate(weaponToInstantiate, insta.position, instance, weaponRoot.transform);
                    break;

                }
                i++;
            }
            Destroy(gameObject);
        }
    }
}
