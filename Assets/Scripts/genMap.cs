using UnityEngine;

public class GenMap : MonoBehaviour
{
    public GameObject cell;
    public GameObject rock;
    GameObject map;
    Vector3 pos;

    // Start is called before the first frame update
    void Awake()
    {
        float space = 2.3f;
        map = new GameObject("map");
        pos = new Vector3(0, 0, 0);
        for (int i = 0; i < Board.Instance.height; i++)
        {
            for (int j = 0; j < Board.Instance.width; j++)
            {
                GameObject o;
                if (Random.Range(0,5)==4)
                    o = Instantiate(rock, pos, Quaternion.identity, map.transform);
                else
                    o = Instantiate(cell, pos, Quaternion.identity, map.transform);
                o.GetComponent<Cell>().x = j;
                o.GetComponent<Cell>().y = i;
                o.name = j.ToString() + ',' + i.ToString();
                Board.Instance.cellList[j + i * Board.Instance.width] = o;
                pos.x += space;
            }
            pos.x = 0;
            pos.z += space;
        }
        Board.Instance.Enemys.Add((GameObject)Instantiate(Resources.Load("Prefabs/Enemy")));
        Board.Instance.pieces.Add((GameObject)Instantiate(Resources.Load("Prefabs/Piece")));
    }
}
