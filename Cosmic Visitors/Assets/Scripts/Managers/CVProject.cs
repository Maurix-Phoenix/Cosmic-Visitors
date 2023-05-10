using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossAttackBehaviour
{
    Normal,
    OrbitShots,
    ChargedShots,
    BulletHell,
}

public enum BossPhases
{
    None = -1,
    Discending = 0,
    Phase1 = 1,
    Phase2 = 2,
    Phase3 = 3,
    Dying = 4,
    ALL = 5,
}

public static class CVProject
{
    public static Vector3 RandomPointOnCircumference(Vector3 center, float radius)
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        float x, y, z;

        x = radius * Mathf.Sin(angle);
        y = radius * Mathf.Cos(angle);
        z = 0;

        return new Vector3(center.x + x,center.y + y,center.z + z);
    }
}
