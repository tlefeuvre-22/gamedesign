using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangePiece : PlayerPiece
{
    private void Start()
    {
        attackRange += 2;
    }
    public override void specialAttack(GameObject cell)
    {
        /*GameObject target = cell.GetComponent<Cell>().occupier;
        if (target != null)
        {
            int x = cell.GetComponent<Cell>().x;
            int y = cell.GetComponent<Cell>().y;
            List<Cell> attackedC = new List<Cell>{ cell.GetComponent<Cell>()};
            if ((x == coordinate[0] || y == coordinate[1]))
            {
                //TODO chain atttack
                List<GameObject> cellInR = new List<GameObject>();
                CheckReachableCells(attackedC[0], cellInR, 1);
                Queue<Cell> cellToCheck = new Queue<Cell>();
                foreach (GameObject tmp in cellInR)
                cellToCheck.Enqueue(tmp.GetComponent<Cell>());
                while(cellToCheck.Count > 0)
                {
                    Cell tmp = cellToCheck.Dequeue();
                    if (tmp.occupier != null && tmp.occupier.TryGetComponent<Piece>(out Piece p))
                    {
                        if (!attackedC.Contains(tmp))
                        { 
                            p.ApplyDamage(1);
                            attackedC.Add(tmp);
                            CheckReachableCells(tmp, cellInR, 1);
                            foreach (GameObject c in cellInR)
                                cellToCheck.Enqueue(c.GetComponent<Cell>());
                        }
                    }
                }
                canAttack = false;
            }
        }*/
    }
}
