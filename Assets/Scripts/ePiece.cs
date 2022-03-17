using UnityEngine;

public class ePiece : MonoBehaviour
{
    int life = 3;
    int[] coordinate = new int[2] { 4, 5 };
    float up = 1.25f;

    // Start is called before the first frame update
    void Start()
    {
        Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width].tag = "busyCell";
        this.transform.position = Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width].transform.position
            + new Vector3(0, up, 0);
        Board.Instance.cellList[coordinate[0] + coordinate[1] * Board.Instance.width].GetComponent<cell>().occupier = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
            die();
    }
    public void playTurn()
    {
        Debug.Log("Enemy Play");
    }
    void die()
    {
        Destroy(gameObject);
    }
    public void applyDamage(int damage)
    {
        life -= damage;
    }
}
