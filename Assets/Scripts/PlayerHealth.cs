using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    //Maybe add some bleeding afterwards
    public float health = 10f;
    public Slider barSlider = null;
    float maxHealth;
    private void Start()
    {
        float maxHealth = health;
        barSlider.value = CalculateHealth();
    }
    // Start is called before the first frame update
    public void DealDamageToPlayer(float damage)
    {
        health -= damage;
        barSlider.value = CalculateHealth();
        if (health <= 0f)
        {

            Die();
            //and call animation
        }
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }
    public void RestoreHealth()
    {
        health = maxHealth;
    }
    void Die()
    {
        Debug.Log(gameObject.name + " is Dead");
    }
}
