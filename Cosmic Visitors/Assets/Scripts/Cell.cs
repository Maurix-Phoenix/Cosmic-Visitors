using UnityEngine;

public class Cell : MonoBehaviour
{
    public int ID;
    public char CharType;
    public bool CanSpawnAlien = false;
    public Vector2Int Position;
    public Vector3 MoveDirection = new Vector3Int();
}
