using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;


public class Stage : MonoBehaviour
{
    public int ID;


    public void GenerateGrid(int rangeSizeX, int rangeSizeY)
    {
        //creating the grid
        for (int i = 0; i < rangeSizeY * 2; i++)
        {
            for (int j = 0; j < rangeSizeX * 2; j++)
            {
                Vector2Int cellPos = new Vector2Int(-rangeSizeX + j, rangeSizeY - i); //from top left to bottom right
                Cell cell = new GameObject().AddComponent<Cell>();
                StageManager.Instance.Cells.Add(cell);
                cell.ID = StageManager.Instance.Cells.IndexOf(cell);
                cell.Position = cellPos;

                float mX, mY;

                if (cell.Position.y != -rangeSizeY)
                {
                    if (cell.Position.y % 2 == 0)
                    {
                        if (cell.Position.x == rangeSizeX-1)
                        {
                            mX = 0;
                            mY = -1;
                        }
                        else
                        {
                            mY = 0;
                            mX = 1;
                        }
                    }
                    else
                    {
                        if (cell.Position.x == -rangeSizeX)
                        {
                            mX = 0;
                            mY = -1;
                        }
                        else
                        {
                            mY = 0;
                            mX = -1;
                        }
                    }
                    cell.MoveDirection = new Vector3(mX, mY);
                }
                else
                {
                    cell.MoveDirection = Vector3.zero;
                }

                if (cell.Position.y > 2)
                {
                    cell.canSpawnAlien = true;
                    StageManager.Instance.UsableCells.Add(cell);
                }

                cell.transform.position = new Vector3(cellPos.x, cellPos.y);
                cell.transform.SetParent(transform);
                cell.gameObject.name = $"cell[{cellPos.x}][{cellPos.y}]";
            }
        }
    }

    public void GenerateStage(int id)
    {
        ID = id;

        if(id % 5 == 0)
        {
            //boss battle
            SpawnBoss(StageManager.Instance.AlienBossTemplate.AlienBossPrefab, StageManager.Instance.AlienBossSpawnPos);
        }
        else
        {
            //SpawnAliens
            SpawnAliens(Random.Range(10,20) * id);
        }
        EventManager.RaiseOnStageGenerated();
    }

    public void SpawnAliens(int number)
    {
        List<Cell> usableCellsClone = new List<Cell>();
        usableCellsClone.Clear();
        for(int i = 0; i< StageManager.Instance.UsableCells.Count; i++ )
        {
            usableCellsClone.Add(StageManager.Instance.UsableCells[i]);
        }
        ///////
        
        for (int i = 0; i < number; i++)
        {
            if(i < usableCellsClone.Count)
            {
                Cell alienCell = usableCellsClone[Random.Range(0, usableCellsClone.Count)];
                usableCellsClone.Remove(alienCell);


                Alien newAlien;
                if (Random.value <= 0.8)
                {
                    newAlien = Instantiate(StageManager.Instance.AlienTemplates[0].AlienPrefab).GetComponent<Alien>();
                    newAlien.AT = StageManager.Instance.AlienTemplates[0];
                }
                else
                {
                    newAlien = Instantiate(StageManager.Instance.AlienTemplates[1].AlienPrefab).GetComponent<Alien>();
                    newAlien.AT = StageManager.Instance.AlienTemplates[1];
                }
                StageManager.Instance.Aliens.Add(newAlien);
                newAlien.CurrentCell = alienCell;

                newAlien.transform.SetParent(transform);
                newAlien.transform.position = alienCell.transform.position;
            }
            else
            {
                Debug.Log("Stage: the number of alien is too great for the grid some alien will not spawn.");
            }

        }
    }
    public void SpawnBoss(GameObject bossPrefab, Vector3 bossSpawnPos)
    {
        StageManager.Instance.AlienBoss = Instantiate(bossPrefab, bossSpawnPos, Quaternion.identity).GetComponent<AlienBoss>();
    }
}
