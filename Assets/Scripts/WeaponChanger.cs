using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class WeaponChanger : MonoBehaviour
    {
        [SerializeField] int currentWeapon = 0;
        public GameObject[] weaponArray = null;
        // Start is called before the first frame update
        void Start()
        {
            SwitchWeapon();
            
        }

        // Update is called once per frame
        void Update()
        {
            NumberWeaponSwitch();
            CycleWeapons();
            if (Input.GetKeyDown(KeyCode.F))
            {


                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }


            }
            
            


        }
        void SwitchNumber(int weaponToswitch)
        {
            int i = 0;
            foreach (GameObject weapon in weaponArray)
            {
                weaponArray[i] = weapon.gameObject;
                if (weaponArray[i] == weaponArray[weaponToswitch])
                {
                    weaponArray[weaponToswitch].gameObject.SetActive(true);
                }
                else
                {
                    weaponArray[i].gameObject.SetActive(false);
                }
                i++;
            }
        }

        void NumberWeaponSwitch()
        {
            //Will improve it later
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                //SwitchNumber(0);
                //currentWeapon = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            {
                SwitchNumber(1);
               // currentWeapon = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
            {
                //SwitchNumber(2);
                //currentWeapon = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
            {
                //SwitchNumber(3);
                //currentWeapon = 3;
            }

        }
        private void CycleWeapons()
        {
            int previousWeapon = currentWeapon;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (currentWeapon >= transform.childCount - 1)
                {
                    currentWeapon = 0;
                }
                else
                {
                    currentWeapon++;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (currentWeapon <= transform.childCount - 1)
                {
                    currentWeapon = 0;
                }
                else
                {
                    currentWeapon--;
                }
            }
            if (previousWeapon != currentWeapon)
            {
                SwitchWeapon();
            }
        }

        void SwitchWeapon()
        {
            int i = 0;
            foreach (Transform weapon in transform)
            {
                if (i == currentWeapon)
                {
                    weapon.gameObject.SetActive(true);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }
               
                i++;
            }
        }
        public GameObject GetWeaponByIndex(int getterIndex)
        {
            return weaponArray[getterIndex];
            //Will exploit it later on
        }
        public GameObject[] GetWeaponArray()
        {
            return weaponArray;
        }
    }
}
