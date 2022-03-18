using UnityEngine;

public class EnemyPiece : Piece
{
    GameObject c;
    // Start is called before the first frame update
    void Start()
    {
        coordinate = new int[2]{ 4, 5 };
        Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width].tag = "busyCell";
        this.transform.position = Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width].transform.position
            + new Vector3(0, up, 0);
        Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width].GetComponent<Cell>().occupier = gameObject;
    }
    protected void Die()
    {
        Board.Instance.Enemys.Remove(gameObject);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
            Die();
    }
    private bool CheckCoordinate(int x, int y)
    {
        return x > 0 && x < Board.Instance.width && y > 0 && y < Board.Instance.height;
    }
    public void NextAttack()
    {
        int range = 1;
        int x ;
        int y;
        for (int nbTries = 10; nbTries > 0; nbTries--)
        {
            x = coordinate[0] + Random.Range(-range, range+1);
            y = coordinate[1] + Random.Range(-range, range+1);
            if (!(x==coordinate[0]&&coordinate[1]==y))
            {
                c= Board.Instance.cellList[x + y * Board.Instance.width];
                c.GetComponent<Renderer>().material.color = Color.red;
                GameObject target = c.GetComponent<Cell>().occupier;
                if (target != null)
                {
                    target.GetComponent<EnemyPiece>().ApplyDamage(1);
                }
                break;
            }
        }
    }
    public void Attack()
    {
        if (c != null)
        {
            GameObject target = c.GetComponent<Cell>().occupier;
            if (target != null)
            {
                target.GetComponent<PlayerPiece>().ApplyDamage(1);
            }
            c.GetComponent<Renderer>().material.color = new Color(0f, 0.5f, 0f);
        }
    }
    public void PlayTurn()
    {
        
        for(int nbTries = 10; nbTries > 0; nbTries--)
        {
            int x = coordinate[0] + Random.Range(-defMouvPoints, defMouvPoints);
            int y = coordinate[1] + Random.Range(-defMouvPoints, defMouvPoints);
            if (CheckCoordinate(x, y))
            {
                GameObject dest = Board.Instance.cellList[x + y * Board.Instance.width];
                if (dest.tag == "cell")
                {
                    MoveTo(dest);
                    mouvPoints = defMouvPoints;
                    break;
                }
            }
        }
        NextAttack();
    }
   
}
