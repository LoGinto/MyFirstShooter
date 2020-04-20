using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DestinationChange : MonoBehaviour
{
    public int xPos;
    public int zPos;
    const float yPos = 257.6634f;//should not move towards Y
    [SerializeField] int xposRange1 = -2,xposRange2 = 50;
    [SerializeField] int zposRange1 = 350, zposRange2 = 300;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            xPos = Random.Range(xposRange1, xposRange2);
            zPos = Random.Range(zposRange1, zposRange2);
            this.gameObject.transform.position = new Vector3(xPos, yPos, zPos);
            
            
        }
    }
   
}
