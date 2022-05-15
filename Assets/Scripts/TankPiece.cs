using UnityEngine;

public class TankPiece : PlayerPiece
{
    void Start()
    {
        defLife = defLife + 2;
        attackRange--;
    }
    public override void specialAttack(GameObject cell)
    {
        /*GameObject target = cell.GetComponent<Cell>().occupier;
        if (target != null)
        {
            int x = cell.GetComponent<Cell>().x;
            int y = cell.GetComponent<Cell>().y;
            if (x == coordinate[0] || y == coordinate[1])
            {
                Cell cc = Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width].GetComponent<Cell>();
                foreach (var c in Utility.FindCells(cc, 1))
                {
                    if (c.occupier != null)
                    {
                        if (c.TryGetComponent(out Piece p))
                            p.mouvPoints = 0;
                    }
                }
                canAttack = false;
            }
        }*/
    }
}
