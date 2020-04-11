using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnRain : MonoBehaviour
{
    [SerializeField] SkyBoxScript skyBoxScript;

    private void Update()
    {
        gameObject.SetActive(skyBoxScript.GetRain());
    }
}
