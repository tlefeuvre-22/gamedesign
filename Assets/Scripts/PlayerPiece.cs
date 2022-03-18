using UnityEngine;
using System.Collections.Generic;
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
        life = 2;
        rend = GetComponent<Renderer>();
        GameObject c = Board.Instance.cellList[coordinate[0] + (coordinate[1] * Board.Instance.width)];
        this.transform.position = c.transform.position + new Vector3(0, up, 0);
        c.tag = "busyCell";
        c.GetComponent<Cell>().occupier = gameObject;
    }
    protected void Die()
    {
        Board.Instance.pieces.Remove(gameObject);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
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
    public void DisplayC()
    {
        cl = new List<Cell>{};
        foreach (GameObject cell in Board.Instance.cellList)
        {
            Cell cs = cell.GetComponent<Cell>();
            if(cs.x==coordinate[0]||cs.y==coordinate[1])
            {
                cl.Add(cs);
                cs.GetComponent<Renderer>().material.color = Color.yellow;
            } 
        }
    }
    public void ClearDisplay() {
        foreach (Cell cell in cl)
        {
            cell.GetComponent<Renderer>().material.color = new Color(0f,0.5f,0f);
        }
    }
    public void Attack(GameObject cell)
    {
        int range = 3;
        GameObject target = cell.GetComponent<Cell>().occupier;
        if (target != null)
        {
            int x = cell.GetComponent<Cell>().x;
            int y = cell.GetComponent<Cell>().y;
            //if (CheckDistance(x, y, coordinate[0], coordinate[1], range))
            if(x == coordinate[0] || y == coordinate[1])
            {
                target.GetComponent<EnemyPiece>().ApplyDamage(1);
                canAttack = false;
            }
        }
    }
   
}
