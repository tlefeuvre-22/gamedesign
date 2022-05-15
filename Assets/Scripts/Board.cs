using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//TODO POWERUP camera
public class Board: Singleton<Board>
{
    public int width;
    public int height;
    public GameObject[] cellList;
    public int nbCell;
    public float nbHeightLevels = 8f;
    public List<GameObject> Enemys = new List<GameObject> { };
    public List<GameObject> pieces = new List<GameObject> { };
    public float[,] noiseMap;
    public void GenBoard(){
        nbCell = width * height;
        cellList = new GameObject[nbCell];
    }
}

