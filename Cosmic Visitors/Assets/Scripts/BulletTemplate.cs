using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="New Bullet", menuName ="Scriptable Objects/Bullets/New Bullet")]
public class BulletTemplate : ScriptableObject
{
    public GameObject BulletPrefab;
    public string Name;
    public float Speed;
    public float Damage;

    public bool Bounce;
    public bool FollowEnemies;
    public bool SplitOnHit;
    public bool OnHitOneKill;

}
