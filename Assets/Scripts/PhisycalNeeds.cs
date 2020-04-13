using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhisycalNeeds : MonoBehaviour
{
    //will configure those 
    [Header("Needs")]
    public float stomachVolume = 100f;
    [SerializeField] float wakePower = 20f;
    [SerializeField] float waterNeed = 15f;
     float maximumHungerLimit;
     float maximumNeedToSleepLimit;
     float maximumNeedForWater;
    [Space(5)]
    [Header("Need Icons")]
    [SerializeField] Image sleepImage;
    [SerializeField] Image hungerImage;
    [SerializeField] Image waterImage;
    [Space(5)]
    [Header("Sounds for drinking and eating")]
    //[SerializeField]AudioSource sourceOfAudio;
    [SerializeField] AudioClip eatingSound;
    [SerializeField] AudioClip drinkingAudioClip;
    public bool foodClick = false;
    private void Start()
    {
        maximumHungerLimit = stomachVolume;
        print(maximumHungerLimit+ " at begininng");
        maximumNeedForWater = waterNeed;
        maximumNeedToSleepLimit = wakePower;
        //sourceOfAudio = GetComponent<AudioSource>();
        //if (sourceOfAudio == null)
        //{
        //    sourceOfAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        //}
    }
    

    private void Update()
    {
        //Substract the variables according to time
        stomachVolume -= Time.deltaTime;
       
        
        if (stomachVolume < 3f)
        {
            hungerImage.gameObject.SetActive(true);
        }
        else
        {
            hungerImage.gameObject.SetActive(false);
        }
        
         
    }



    public void Eat(float amt)
    {

        FindObjectOfType<PhisycalNeeds>().stomachVolume += amt;
    }
    public void Drink()
    {
        waterNeed += maximumNeedForWater/2;
        
     //   audioSource.PlayOneShot(drinkingAudioClip);
       
    }
    public void Sleep()
    {
        // must ADD some more functionality here,for now just replenish 
        wakePower = maximumNeedToSleepLimit;

    }
   

}
