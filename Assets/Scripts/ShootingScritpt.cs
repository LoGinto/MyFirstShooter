using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Weapon
{
    public class ShootingScritpt : MonoBehaviour
    {
        [Header("Range and Damage")]
        public float range = 100f;
        public float damage = 10f;
        [Header("Bullet Variables")]
        public int maxBulletLoaded = 6;
        private int currentBullets;
        public int carryingOfType = 18;
        public bool isAutomatic = false;
        //public float caliber = 24f;
        [Space(10)]
        [SerializeField] Camera shootCam;
        [Space(10)]
        [Header("Reload Variables")]
        public bool hasReloadAnimation = false;//sadly
        public float reloadTime = 2f;
        [Header("Animation and Sound ")]
        Animator animator;
        AudioSource audioSource;
        [Space(5)]
        public AudioClip shootSFX;
        public AudioClip jamSFX;
        public AudioClip reloadSound = null;
        [Space(5)]
        [Header("Variables for UI")]
        public Text bulletsInMagazineText = null;
        public Text amountOfBulletsIHaveFoundText;

        // Start is called before the first frame update
        void Start()
        {
            currentBullets = maxBulletLoaded;
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            if(shootCam == null)
            {
                shootCam = GameObject.FindGameObjectWithTag("FPSCam").GetComponent<Camera>();
            }
            if (bulletsInMagazineText == null)
            {
                bulletsInMagazineText = GameObject.FindGameObjectWithTag("CurrentBullets").GetComponent<Text>();
            }
            if (amountOfBulletsIHaveFoundText == null)
            {
                amountOfBulletsIHaveFoundText = GameObject.FindGameObjectWithTag("MaxBullets").GetComponent<Text>();
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (isAutomatic == false)
            {
                Fire();
            }
            else
            {
                FireContiniously();
            }

            ShowBulletsOnUI();
            if (Input.GetKeyDown(KeyCode.R))
            {

                Reload();//To do


            }
        }
        private void ShowBulletsOnUI()
        {
            bulletsInMagazineText.text = currentBullets.ToString();
            amountOfBulletsIHaveFoundText.text = carryingOfType.ToString();
        }

        private void Fire()
        {
            if (Input.GetButtonDown("Fire1"))//implement additional stuff
            {

                currentBullets--;
                if (currentBullets > 0)
                {
                    audioSource.PlayOneShot(shootSFX);
                    LetTheProjectilesOut();
                }
                else
                {
                    Reload();
                }
            }
        }
        private void FireContiniously()
        {
            if (Input.GetButton("Fire1"))//implement additional stuff
            {

                currentBullets--;
                if (currentBullets > 0)
                {
                    audioSource.PlayOneShot(shootSFX);
                    LetTheProjectilesOut();
                }
                else
                {
                    Reload();
                }
            }
        }

        private void Reload()
        {
            //Pressing R
            // So for example 4/20 and I need to put 2 more bullets
            // 6/18 is needed to implement it -> remains = max - current(I get that I need 2 bullets)
            //carryingOftype-= remains and ->  do as previously currentbullets = maxBullets 
            if (carryingOfType > 0)
            {
                //carryingOfType -= maxBulletLoaded;
                int remains = maxBulletLoaded - currentBullets;
                carryingOfType -= remains;
                if (hasReloadAnimation == true)
                {
                    PlaySound();
                    StartCoroutine("ReloadAnim");
                }
                else
                {
                    PlaySound();
                    StartCoroutine("ReloadWait");
                }
            }
            else
            {
                audioSource.PlayOneShot(jamSFX);
                Debug.Log("No More Bullets Left");
            }
        }

        void LetTheProjectilesOut()
        {
            animator.SetTrigger("Shoot");
            RaycastHit hit;

            if (Physics.Raycast(shootCam.transform.position, shootCam.transform.forward, out hit, range))
            {
                Health target = hit.transform.GetComponent<Health>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                    Debug.Log("Target is shot and took" + damage + " damage");
                }
            }
        }

        IEnumerator ReloadAnim()
        {
            animator.SetBool("Reload", true);
            PlaySound();
            currentBullets = maxBulletLoaded;
            yield return new WaitForSeconds(reloadTime);
            animator.SetBool("Reload", false);

        }
        IEnumerator ReloadWait()
        {

            yield return new WaitForSeconds(reloadTime);
            currentBullets = maxBulletLoaded;

        }
        void PlaySound()//shit animation event
        {
            audioSource.PlayOneShot(reloadSound);
        }
        public void AddFoundBullets(int bulletsFound)
        {
             carryingOfType =+ bulletsFound;
        }
        
       
    }
}