using UnityEngine;
using System.Collections.Generic;
public class Barrel : MonoBehaviour
{
    int damage = 1;
    Cell currentCell;
    bool isExploding = false;
    void Start()
    {
        currentCell = transform.parent.GetComponent<Cell>();
    }
    public void Explode()
    {
        if (isExploding)
            return;
        isExploding = true;
        List<Cell> cells = new List<Cell>();
        Utility.FindCells(currentCell, 2, null, cells);
        foreach (var c in cells)
        {
            if (c.occupier != null)
            {
                if (c.occupier.TryGetComponent(out Barrel b))
                    b.Explode();
                if (c.occupier.TryGetComponent(out Piece p))
                    p.ApplyKnockBack(currentCell.x, currentCell.y, p, damage);
            }
        }
        currentCell.occupier = null;
        currentCell.Barrel.SetActive(false);
    }
}
