using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWays : MonoBehaviour
{
    //visualisation 
    const float endPointRadius = 0.3f;

    public int GetNextIndexInLoop(int i)
    {
        if (i + 1 == transform.childCount)
        {
            return 0;
        }
        return i + 1;
    }
    public Vector3 GetEndpoint(int i)
    {
        return transform.GetChild(i).position;
    }
    private void OnDrawGizmos()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            int j = GetNextIndexInLoop(i);
            Gizmos.DrawSphere(GetEndpoint(i), endPointRadius);
            Gizmos.DrawLine(GetEndpoint(i), GetEndpoint(j));
        }
    }
}
