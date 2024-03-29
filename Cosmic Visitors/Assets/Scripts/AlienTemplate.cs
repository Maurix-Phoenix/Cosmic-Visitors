using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Alien", menuName = "Scriptable Objects/Aliens/Create New Alien Template")]
public class AlienTemplate : ScriptableObject
{
    public int Health;
    public float Speed;
    public float FireRate;
    public float FireRateVariation = 1f;
    public GameObject AlienPrefab;
    public BulletTemplate Bullet;

    public int DrawOrder;

    public bool canShoot;
}
