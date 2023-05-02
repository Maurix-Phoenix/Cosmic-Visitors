using UnityEngine;

public class Cell : MonoBehaviour
{
    public int ID;
    public bool canSpawnAlien = false;
    public Vector2Int Position;

    public Vector3 MoveDirection = new Vector3Int();
}
