using System;
using System.Collections.Generic;
using UnityEngine;
//one that do chain attack
//one like telport
//one that block mouvement og ennemy
public class PlayerPiece : Piece
{
    bool selected = false;
    Color colorNSelected = Color.blue;
    Color colorSelected = Color.cyan;
    Renderer rend;
    List<Cell> cl = new List<Cell> { };

    // Start is called before the first frame update
    void Start()
    {
        _life = defLife;
        rend = GetComponent<Renderer>();
        def = GetComponent<Renderer>().material.color;
    }
    protected void Die()
    {
        Board.Instance.pieces.Remove(gameObject);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (_life <= 0)
            Die();
        if (selected)
            rend.material.color = colorSelected;
        else
            rend.material.color = colorNSelected;


    }
    public void ToggleSelect()
    {
        selected = !selected;
    }
    public void DisplayMouv()
    {
        ClearDisplay();
        if (!canAttack)
            return;
        GameObject currentCell = Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width];
        cl.Clear();
        Utility.FindCells(currentCell.GetComponent<Cell>(), mouvPoints, cl);
        foreach (Cell cell in cl)
        {
            if(cell.IsFree())
                cell.GetComponent<Renderer>().material.color = Color.green;
        }
    }
    public void DisplayC()
    {
        ClearDisplay();
        foreach (GameObject cell in Board.Instance.cellList)
        {
            Cell cs = cell.GetComponent<Cell>();
            if ((cs.x == coordinate[0] || cs.y == coordinate[1]) && CheckDistance(cs.x, cs.y, attackRange))
            {
                cl.Add(cs);
                cs.GetComponent<Renderer>().material.color = Color.yellow;
                //TODO becarful if the cell is red
            }
        }
    }
    bool CheckDistance(int x, int y, int max)
    {
        return Utility.Abs(coordinate[0] - x) + Utility.Abs(coordinate[1] - y) < max;
    }
    public void ClearDisplay()
    {
        foreach (Cell cell in cl)
        {
            if (cell.type == CellType.land)
                cell.GetComponent<Renderer>().material.color = new Color(0f, 0.5f, 0f);
            else
                cell.GetComponent<Renderer>().material.color = Color.blue;
        }
        cl = new List<Cell> { };
    }
    public void Attack(GameObject cell)
    {
        GameObject target = cell.GetComponent<Cell>().occupier;
        if (target != null)
        {
            int x = cell.GetComponent<Cell>().x;
            int y = cell.GetComponent<Cell>().y;
            if ((x == coordinate[0] || y == coordinate[1]) && CheckDistance(x, y, attackRange))
            {
                if (target.TryGetComponent(out EnemyPiece e))
                {
                    e.ApplyDamage(1);
                    e.ApplyKnockBack(coordinate[0], coordinate[1], e, 1);
                }
                if (target.transform.TryGetComponent(out Barrel b))
                {
                    b.Explode();
                }
                canAttack = false;
            }
        }
    }
    public virtual void specialAttack(GameObject cell)
    {
        throw new NotImplementedException();
    }
}


