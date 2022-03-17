using UnityEngine;

public class genMap : MonoBehaviour
{
    public GameObject cell;
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
                GameObject o = Instantiate(cell, pos, Quaternion.identity, map.transform);
                o.GetComponent<cell>().x = j;
                o.GetComponent<cell>().y = i;
                o.name = j.ToString() + ',' + i.ToString();
                Board.Instance.cellList[j + i * Board.Instance.width] = o;
                pos.x += space;
            }
            pos.x = 0;
            pos.z += space;
        }
        Board.Instance.enemys[0] = (GameObject)Instantiate(Resources.Load("Prefabs/Enemy"));
        Board.Instance.pieces[0] = (GameObject)Instantiate(Resources.Load("Prefabs/Piece"));
    }
}
