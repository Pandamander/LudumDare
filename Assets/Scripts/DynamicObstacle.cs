using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DynamicObstacleDamageType
{
    SpinNoSteering,
    LowTraction,
    Slow
}

public interface IDynamicObstacle
{
    DynamicObstacleDamageType DamageType { get; }
    float DamageDuration { get; }
}
