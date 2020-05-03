using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Weapon
{
    public class BulletPickup : MonoBehaviour
    {
        public int bulletAmount = 10;
        public string weaponForPickup;
        GameObject weaponRoot;
        private void Start()
        {
            weaponRoot = GameObject.FindGameObjectWithTag("WeaponRoot");
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                foreach (Transform weapon in weaponRoot.transform)
                {
                    
                        if (weapon.gameObject.GetComponent<ShootingScritpt>().GetWeaponType() == weaponForPickup)
                        {
                            weapon.gameObject.GetComponent<ShootingScritpt>().AddFoundBullets(bulletAmount);
                            break;
                        }                    
                }
                Destroy(gameObject);
            }
        }

    }
}