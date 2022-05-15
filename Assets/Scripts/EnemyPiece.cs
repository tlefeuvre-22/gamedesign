using System.Collections.Generic;
using UnityEngine;
public class EnemyPiece : Piece
{
    GameObject targetCell;
    public bool alredySpawn = false;
    public int ID;
    [ExecuteInEditMode]
    public void AddId()
    {
        ID = Random.Range(0, 1 << 63);
    }
    void Start()
    {
        def = GetComponent<Renderer>().material.color;

        /*while (true && !alredySpawn)
        {
           
            GameObject c = Board.Instance.cellList[Random.Range(0, Board.Instance.cellList.Length)];
            if (c.GetComponent<Cell>().IsFree())
            {
                Move(c);
                break;
            }
        }
        alredySpawn = true;*/

    }
    protected void Die()
    {
        Board.Instance.Enemys.Remove(gameObject);
        Destroy(gameObject);
    }

    void Update()
    {
        if (_life <= 0)
            Die();
    }

    public void NextAttack()
    {
        int range = 1;
        int x;
        int y;
        Cell currentCell = Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width].GetComponent<Cell>();
        List<Cell> targets =new List<Cell>();
        Utility.FindCells(currentCell, attackRange, null, targets);
        foreach (Cell c in targets)
        {
            if (c.occupier!=null && c.occupier.TryGetComponent<PlayerPiece>(out _))
            {
                targetCell = c.gameObject;
                targetCell.GetComponent<Renderer>().material.color = Color.red;
                return;
            }
        }
        for (int nbTries = 10; nbTries > 0; nbTries--)
        {
            x = coordinate[0] + Random.Range(-range, range + 1);
            y = coordinate[1] + Random.Range(-range, range + 1);

            if (x >= 0 && x < Board.Instance.width && y >= 0 && y < Board.Instance.height)
            {
                if (!(x == coordinate[0] && coordinate[1] == y))
                {
                    targetCell = Board.Instance.cellList[x + y * Board.Instance.width];
                    targetCell.GetComponent<Renderer>().material.color = Color.red;
                    break;
                }
            }
        }
    }
    public void Attack()
    {
        if (targetCell != null)
        {
            GameObject target = targetCell.GetComponent<Cell>().occupier;
            if (target != null)
            {
                if (target.TryGetComponent(out PlayerPiece p))
                {
                    p.ApplyDamage(1);
                    p.ApplyKnockBack(coordinate[0], coordinate[1], p, 1);
                }
                if (target.transform.TryGetComponent(out Barrel b))
                {
                    b.Explode();
                }
            }
            if (targetCell.GetComponent<Cell>().type == CellType.land)
                targetCell.GetComponent<Renderer>().material.color = new Color(0f, 0.5f, 0f);
            else
                targetCell.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
    int CalcDistance(int x, int y)
    {
        return Utility.Abs(coordinate[0] - x) + Utility.Abs(coordinate[1] - y);
    }
    int CalcD2Pt(int x,int y, int x1, int y1)
    {
        return Utility.Abs(x1 - x) + Utility.Abs(y1 - y);
    }
    private Cell FPP(List<Cell> cells)
    {
        Cell target = null;
        if (cells.Count > 0)
        {
            int min = 0;
            target = cells[0];
            foreach (Cell cell in cells)
            {
                int d = CalcDistance(cell.x, cell.y);
                if (d < min)
                {
                    min = d;
                    target = cell;
                }
            }
        }
        return target;
    }
    private void MoveToPPiece(List<Cell> rC, Cell target)
    {
        int min = CalcD2Pt(rC[0].x, rC[0].y, target.x, target.y);
        Cell minC = rC[0];
        foreach (Cell c in reachableCells)
        {
            if (CalcD2Pt(c.x, c.y, target.x, target.y) < min)
                minC = c;
        }
        MoveTo(minC.gameObject);
        return;
    }
    public void PlayTurn()
    {
        Attack();
        //TODO
        /*int viewRange = 10;
        List<Cell> cells = new List<Cell>();
        List<Cell> rC = new List<Cell>();
        Cell currentCell = Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width].GetComponent<Cell>();
        Utility.FindCells(currentCell, viewRange, rC, cells);
        Cell target = FPP(cells);
        if (target != null && rC.Count > 0)
        {
            MoveToPPiece(rC,target);
            return;
        }
        else
        {*/
        for (int nbTries = 10; nbTries > 0; nbTries--)
        {
            int x = coordinate[0] + Random.Range(-defMouvPoints, defMouvPoints);
            int y = coordinate[1] + Random.Range(-defMouvPoints, defMouvPoints);
            if (CheckCoordinate(x, y))
            {
                GameObject dest = Board.Instance.cellList[x + y * Board.Instance.width];
                if (dest.GetComponent<Cell>().IsFree())
                {
                    MoveTo(dest);
                    break;
                }
            }
        }
        //}
        mouvPoints = defMouvPoints;
        NextAttack();
    }

}
