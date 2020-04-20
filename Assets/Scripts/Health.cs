using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    [Header("Public health variables")]
    public float health = 10f;
    //public Slider barSlider = null;
    float maxHealth;
    [Space(5)]
    [Header("Sounds")]
    [SerializeField] AudioClip[] injurySounds;
    [SerializeField] AudioClip[] deathSounds;
    AudioSource audioSource;
    Animator animator;
    //int timesDamageTaken = 0;
    private void Start()
    {
        float maxHealth = health;
       // barSlider.value = CalculateHealth();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    public void TakeDamage(float damage)
    {
        //timesDamageTaken =+1;
        //if (timesDamageTaken >= 1)
        //{
        //    //Show healthBar
        //}
        health -= damage;
        //barSlider.value = CalculateHealth();
        if (health <= 0f)
        { 
            Die();
        }
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }
    public void RestoreHealth()
    {
        health = maxHealth;
        Debug.Log("Health restored");
    }
    void Die()
    {
        animator.SetTrigger("Die");
        Debug.Log(gameObject + "is Dead");
    }
}
