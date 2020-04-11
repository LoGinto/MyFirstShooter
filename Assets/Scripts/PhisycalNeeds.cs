using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhisycalNeeds : MonoBehaviour
{
    //will configure those 
    [Header("Needs")]
    [SerializeField] float hungerNeed = 20f;
    [SerializeField] float needTosleep = 20f;
    [SerializeField] float waterNeed = 15f;
    private float maximumHungerLimit;
    private float maximumNeedToSleepLimit;
    private float maximumNeedForWater;
    [Space(5)]
    [Header("Need Icons")]
    [SerializeField] Image sleepImage;
    [SerializeField] Image hungerImage;
    [SerializeField] Image waterImage;
    [Space(5)]
    [Header("Sounds for drinking and eating")]
   //  AudioSource audioSource;
    [SerializeField] AudioClip eatingSound;
    [SerializeField] AudioClip drinkingAudioClip;
    private void Start()
    {
        maximumHungerLimit = hungerNeed;
        maximumNeedForWater = waterNeed;
        maximumNeedToSleepLimit = needTosleep;
        //audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }
    

    private void Update()
    {
        //Substract the variables according to time
        hungerNeed -= Time.deltaTime;
        waterNeed -= Time.deltaTime;
        needTosleep -= Time.deltaTime;
        ActionWhileNeedOccured(2f);

    }

    private void ActionWhileNeedOccured(float timeToDoChanges)
    {
        if (hungerNeed <= timeToDoChanges)//will convert it to minutes afterwards
        {
            hungerImage.gameObject.SetActive(true); //for now show image 

        }
        else if(hungerNeed>timeToDoChanges)
        {
            hungerImage.gameObject.SetActive(false);
        }
        if (waterNeed <= timeToDoChanges)
        {
            waterImage.gameObject.SetActive(true);
        }
        else
        {
            waterImage.gameObject.SetActive(false);
        }
        if (needTosleep <= timeToDoChanges)
        {
            sleepImage.gameObject.SetActive(true);
        }
        else
        {
            sleepImage.gameObject.SetActive(false);
        }
    }

    public void Eat()
    {
        hungerNeed += maximumHungerLimit/2;
       
            //audioSource.PlayOneShot(eatingSound);
        
       

    }
    public void Drink()
    {
        waterNeed += maximumNeedForWater/2;
        
     //   audioSource.PlayOneShot(drinkingAudioClip);
       
    }
    public void Sleep()
    {
        // must ADD some more functionality here,for now just replenish 
        needTosleep = maximumNeedToSleepLimit;

    }
   

}
