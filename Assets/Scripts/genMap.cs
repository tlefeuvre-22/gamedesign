using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenMap : MonoBehaviour
{

    public GameObject cell;
    GameObject map;
    Vector3 pos;
    float space = 2.3f;
    public int width = 10;
    public int height = 10;
    [Range(0, 100)]
    public int seed = 0;
    public Material water;
    public int nbObstacles = 0;
    [Range(0, 10)]
    public int nbHeightlevels = 8;
    int nbAccessibleTiles = 0;
    [Range(0, 100)]
    public int obstacleSeed;
    int nbWaterCell =0;

    // Start is called before the first frame update
    void Start()//AWake
    {
        SpawnPieces();
    }
    public void genMap()
    {
        GameObject mapObj = GameObject.Find("map");
        if (mapObj != null)
            DestroyImmediate(mapObj);
        Board.Instance.width = width;
        Board.Instance.height = height;
        Board.Instance.nbHeightLevels = nbHeightlevels;
        Board.Instance.GenBoard();
        nbWaterCell = 0;
        nbAccessibleTiles = Board.Instance.nbCell;
        map = new GameObject("map");
        pos = new Vector3(0, 0, 0);
        Board.Instance.noiseMap = PerlinNoise.GenerateNoiseMap(width, height, seed, 27, 4, 0.5f, 1.87f);

        for (int i = 0; i < Board.Instance.height; i++)
        {
            for (int j = 0; j < Board.Instance.width; j++)
            {
                CreatNewCell(i, j, Board.Instance.noiseMap, space);
            }
            pos.x = 0;
            pos.z += space;
        }
    }

    void CreatNewCell(int i, int j, float[,] noiseMap, float space)
    {
        int h = (int)(noiseMap[i, j] * Board.Instance.nbHeightLevels);
        Vector3 p = new Vector3(pos.x, h, pos.z);
        GameObject o;
        o = Instantiate(cell, p, Quaternion.identity, map.transform);
        Cell c = o.GetComponent<Cell>();
        c.x = j; 
        c.y = i; 
        c.height = h;
        if (c.height == 0)
        {
            c.GetComponent<Renderer>().material = water;
            c.type = CellType.water;
            nbAccessibleTiles--;
            nbWaterCell++;
        }
        o.name = j.ToString() + ',' + i.ToString();
        Board.Instance.cellList[j + i * Board.Instance.width] = o;
        pos.x += space;
    }
    void SpawnPieces()
    {
        for (int nbPieces = 3; nbPieces > 0; nbPieces--)
        {
            //Board.Instance.Enemys.Add((GameObject)Instantiate(Resources.Load("Prefabs/Enemy")));
            Board.Instance.pieces.Add((GameObject)Instantiate(Resources.Load("Prefabs/Piece")));
        }
    }
    private void ClearObstacle()
    {
        NbWaterCellU();
        foreach (GameObject c in Board.Instance.cellList)
        {
            c.GetComponent<Cell>().occupier = null;
            c.GetComponent<Cell>().DiplayObstacle();
            nbAccessibleTiles = Board.Instance.nbCell-nbWaterCell;
        }
    }
    private void NbWaterCellU()
    {
        nbWaterCell = 0;
        foreach (GameObject c in Board.Instance.cellList)
        {
            if (c.GetComponent<Cell>().type == CellType.water)
                nbWaterCell++;
        }
    }
    public void SpawnObstacle()
    {
        ClearObstacle();
        Queue<GameObject> randCell = new Queue<GameObject>(Utility.ShuffleArray(Board.Instance.cellList.ToArray(), obstacleSeed));
        for (int i = 0; i < nbObstacles; i++)
        {
            if (randCell.Count <= 0)
                break;
            GameObject c = randCell.Dequeue();
            if (c.GetComponent<Cell>().type == CellType.water)
                continue;
            nbAccessibleTiles--;

            if (Random.Range(0, 2) == 0)
                c.GetComponent<Cell>().occupier = c.GetComponent<Cell>().Tree;
            else
                c.GetComponent<Cell>().occupier = c.GetComponent<Cell>().Rocks;
            if (!MapIsFullyAccesible(Board.Instance.cellList[0].GetComponent<Cell>()))
            {
                nbAccessibleTiles++;
                c.GetComponent<Cell>().occupier = null;
                continue;
            }
            c.GetComponent<Cell>().DiplayObstacle();
        }

    }

    bool MapIsFullyAccesible(Cell c)
    {
        Queue<Cell> queue = new Queue<Cell>();
        int nbCurrentAccessibleC = 0;
        queue.Enqueue(c);
        bool[] boardFlag = new bool[Board.Instance.nbCell];
        while (queue.Count > 0)
        {
            Cell cell = queue.Dequeue();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int nx = cell.x + x;
                    int ny = cell.y + y;
                    if ((y == 0 || x == 0) && nx >= 0 && nx < Board.Instance.width && ny >= 0 && ny < Board.Instance.height)
                    {
                        int index = nx + ny * Board.Instance.width;
                        Cell cl = Board.Instance.cellList[index].GetComponent<Cell>();
                        if (cl.IsFree() && !boardFlag[index])
                        {
                            queue.Enqueue(Board.Instance.cellList[nx + ny * Board.Instance.width].GetComponent<Cell>());
                            nbCurrentAccessibleC++;
                            boardFlag[index] = true;
                        }
                    }
                }
            }
        }
        return nbCurrentAccessibleC == nbAccessibleTiles;
    }
}
