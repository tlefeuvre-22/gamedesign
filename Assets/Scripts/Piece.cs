using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Piece : MonoBehaviour
{
    protected int _life = 3;
    public int defLife = 3;
    protected Color def;
    public int life
    {
        get => _life;
    }
    public int[] coordinate = new int[2] { 0, 0 };
    protected float up = 1.25f;
    public int defMouvPoints = 3;
    public int mouvPoints = 3;
    public bool canAttack = true;
    protected List<Cell> reachableCells = new List<Cell>();
    protected int attackRange = 3;
    public bool hasShield= false;
    public GameObject shieldGO;
    private void Start()
    {
        _life = defLife;
    }
    /*protected void CheckReachableCells(Cell currentCell, List<GameObject> reachableCell, int maxDist)
    {
        if (maxDist <= 0)
            return;
        for (int i = -1; i < 2; i++)
        {
            int x = currentCell.x + i;
            if (i != 0 && x >= 0 && x < Board.Instance.width)
            {
                GameObject c = global::Board.Instance.cellList[x + currentCell.y * Board.Instance.width];
                Cell cell = c.GetComponent<Cell>();
                if (cell.IsFree())
                {   if(!reachableCell.Contains(c))
                        reachableCell.Add(c);
                    CheckReachableCells(cell, reachableCell, maxDist - 1);
                }
            }
        }
        if (maxDist <= 0)
            return;
        for (int j = -1; j < 2; j++)
        {
            int y = currentCell.y + j;
            if (j != 0 && y >= 0 && y < Board.Instance.height)
            {
                GameObject c = global::Board.Instance.cellList[currentCell.x + y * Board.Instance.width];
                Cell cell = c.GetComponent<Cell>();
                if (cell.IsFree())
                {
                    if (!reachableCell.Contains(c))
                        reachableCell.Add(c);
                    CheckReachableCells(cell, reachableCell, maxDist - 1);
                }
            }
        }
    }*/
    public void MoveTo(GameObject dest, bool teleport = false)
    {
        GameObject cell = Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width];
        Utility.FindCells(cell.GetComponent<Cell>(), defMouvPoints, reachableCells);
        Cell c = dest.GetComponent<Cell>();
        if ((reachableCells.Contains(c) && canAttack && mouvPoints != 0) || teleport)
        {
            LeavCell();
            Move(dest);
            mouvPoints = 0;
            if (this is PlayerPiece && c.chest)
            {
                ChestManager.ReciveObject();
                c.ToggleChest();
            } 
            if (this is PlayerPiece && c.powerUp)
            {
                PowerUp.Effect(this);
                c.ToggleSPowerUP();
            }

        }
    }
    protected void LeavCell()
    {
        GameObject currentCell = Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width];
        currentCell.GetComponent<Cell>().occupier = null;
    }
    public void ApplyKnockBack(int x, int y, Piece p, int damage)
    {
        int x1 = x - coordinate[0];
        int y1 = y - coordinate[1];
        int nx = x1 > 0 ? coordinate[0] - 1 : coordinate[0];
        int ny = y1 > 0 ? coordinate[1] - 1 : coordinate[1];
        if (x1 < 0)
            nx++;
        if (y1 < 0)
            ny++;
        if(CheckCoordinate(nx, ny))
        {
            GameObject c = Board.Instance.cellList[(nx) + (ny) * Board.Instance.width];
            Cell cell = c.GetComponent<Cell>();
            if (cell.IsFree())
            {

                LeavCell();
                Move(c);
            }
            else
            {
                if (cell.type == CellType.water)
                    p.ApplyDamage(p._life);
                else
                    p.ApplyDamage(damage);
            }
        }
        else
        {
            Debug.Log("Bad coordinate!: "+nx+","+ny);
        }
       
        
    }
    protected bool CheckCoordinate(int x, int y)
    {
        return x >= 0 && x < Board.Instance.width && y >= 0 && y < Board.Instance.height;
    }
    public void Move(GameObject dest)
    {
        int x = dest.GetComponent<Cell>().x;
        int y = dest.GetComponent<Cell>().y;
        coordinate[0] = x; coordinate[1] = y;
        GameObject c = Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width];
        transform.position = c.transform.position + new Vector3(0, up, 0);
        c.GetComponent<Cell>().occupier = gameObject;
        reachableCells.Clear();
    }
    public void ApplyDamage(int damage)
    {
        if (!hasShield)
        {
            _life -= damage;
            if(isActiveAndEnabled)
                StartCoroutine(Blink());
        }
        else
        { 
            hasShield = false;
            shieldGO.SetActive(false);
        }
        
    }
    public void RestMovePt()
    {
        mouvPoints = defMouvPoints;
    }
    IEnumerator Blink()
    {
        for (int i = 0; i <= 3; i++)
        {
            GetComponent<Renderer>().material.color = Color.gray;
            yield return new WaitForSeconds(0.2f);
            GetComponent<Renderer>().material.color = def;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
