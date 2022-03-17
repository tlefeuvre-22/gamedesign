using UnityEngine;

public class Piece : MonoBehaviour
{
    bool selected = false;
    Color colorNSelected = Color.blue;
    Color colorSelected = Color.cyan;
    Renderer rend;
    int life = 10;
    int defMouvPoints = 3;
    int mouvPoints = 3;
    public bool canAttack = true;
    int[] coordinate = new int[2] { 0, 0 };
    float up = 1.25f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        GameObject c = Board.Instance.cellList[coordinate[0] + (coordinate[1] * Board.Instance.width)];
        this.transform.position = c.transform.position + new Vector3(0, up, 0);
        c.tag = "busyCell";
        c.GetComponent<cell>().occupier = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
            die();
        if (selected)
            rend.material.color = colorSelected;
        else
            rend.material.color = colorNSelected;

    }
    public void toggleSelect()
    {
        selected = !selected;
    }
    public void restMovePt()
    {
        mouvPoints = defMouvPoints;
    }
    int abs(int x)
    {
        if (x < 0)
            x *= -1;
        return x;
    }
    public void moveTo(GameObject dest)
    {
        int x = dest.GetComponent<cell>().x;
        int y = dest.GetComponent<cell>().y;
        if (checkDistance(x, y, coordinate[0], coordinate[1], mouvPoints) && canAttack)
        {
            GameObject c = Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width];
            c.tag = "cell";
            c.GetComponent<cell>().occupier = null;
            this.transform.position = dest.transform.position + new Vector3(0, up, 0);
            coordinate[0] = x; coordinate[1] = y;
            c = Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width];
            c.tag = "busyCell";
            c.GetComponent<cell>().occupier = gameObject;
            mouvPoints = 0;
        }

    }
    int calcDist(int x1, int y1, int x2, int y2)
    {
        return abs(x1 - x2) + abs(y1 - y2);
    }
    bool checkDistance(int x1, int y1, int x2, int y2, int maxDist)
    {
        return calcDist(x1, y1, x2, y2) <= maxDist;
    }
    public void attack(GameObject cell)
    {
        int range = 3;
        GameObject target = cell.GetComponent<cell>().occupier;
        if (target != null)
        {
            int x = cell.GetComponent<cell>().x;
            int y = cell.GetComponent<cell>().y;
            if (checkDistance(x, y, coordinate[0], coordinate[1], range))
            {
                target.GetComponent<ePiece>().applyDamage(1);
                canAttack = false;
            }
        }
    }
    void die()
    {
        Destroy(gameObject);
    }
}
