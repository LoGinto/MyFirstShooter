using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxScript : MonoBehaviour
{
    [SerializeField] Transform sun;
    [SerializeField] Transform moon;
    [SerializeField] Material daylightSky;
    [SerializeField] Material eveningSky;
    [SerializeField] Material nightSky;
    [SerializeField] Material rainyDay;
    [SerializeField] bool isRainingDuringDay;
    [SerializeField]float dayRainFactor;
    [SerializeField]float dayRainFactorContainer;

    private void Update()
    {
        if (dayRainFactorContainer>=8 && dayRainFactorContainer<=9)
        {
            isRainingDuringDay = true;
        }
        else
        {
            isRainingDuringDay = false;
        }
        ChangeSky();
    }
    void ChangeSky()
    {
        
        if(sun.rotation.x <= 15&&!isRainingDuringDay)
        {
            dayRainFactorContainer = dayRainFactor;
            RenderSettings.skybox = daylightSky;
        }
        if (sun.rotation.x <= 15 && isRainingDuringDay)//skybox change during day only
        {
            dayRainFactorContainer = dayRainFactor;
            RenderSettings.skybox = rainyDay;
        }

        if (moon.rotation.x > 0)
        {
            RenderSettings.skybox = nightSky;
            dayRainFactor = Random.Range(5f, 9f);
        }
        if(sun.rotation.x < 0 && moon.rotation.x < 0)
        {
            RenderSettings.skybox = eveningSky;
        }
        
    }
    public bool GetRain()
    {
        return isRainingDuringDay;
    }

}
