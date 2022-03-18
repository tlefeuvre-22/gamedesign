using UnityEngine;

public class Piece : MonoBehaviour
{
    protected int life = 3;
    protected int[] coordinate = new int[2] { 0, 0 };
    protected float up = 1.25f;
    protected int defMouvPoints = 3;
    public int mouvPoints = 3;
    public bool canAttack = true;

    private int Abs(int x)
    {
        if (x < 0)
            x *= -1;
        return x;
    }
    protected int CalcDist(int x1, int y1, int x2, int y2)
    {
        return Abs(x1 - x2) + Abs(y1 - y2);
    }
    protected bool CheckDistance(int x1, int y1, int x2, int y2, int maxDist)
    {
        return CalcDist(x1, y1, x2, y2) <= maxDist;
    }
    public void MoveTo(GameObject dest)
    {
        int x = dest.GetComponent<Cell>().x;
        int y = dest.GetComponent<Cell>().y;
        if (CheckDistance(x, y, coordinate[0], coordinate[1], mouvPoints) && canAttack)
        {
            GameObject c = Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width];
            c.tag = "cell";
            c.GetComponent<Cell>().occupier = null;
            transform.position = dest.transform.position + new Vector3(0, up, 0);
            coordinate[0] = x; coordinate[1] = y;
            c = Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width];
            c.tag = "busyCell";
            c.GetComponent<Cell>().occupier = gameObject;
            mouvPoints = 0;
        }

    }
    public void ApplyDamage(int damage)
    {
        life -= damage;
    }
    public void RestMovePt()
    {
        mouvPoints = defMouvPoints;
    }
}
