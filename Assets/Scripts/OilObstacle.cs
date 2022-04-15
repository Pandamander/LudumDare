using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilObstacle : MonoBehaviour, IDynamicObstacle
{
    // IDynamicObstacle
    public DynamicObstacleDamageType DamageType
    {
        get
        {
            return DynamicObstacleDamageType.SpinNoSteering;
        }
    }

    public float duration;
    public float DamageDuration
    {
        get
        {
            return duration;
        } 
    }
}
