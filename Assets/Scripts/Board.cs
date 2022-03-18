using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Board: Singleton<Board>
{
    public int width = 10;
    public int height = 10;
    public int nbCell = 100;
    public GameObject[] cellList = new GameObject[100];
    public List<GameObject> Enemys = new List<GameObject> { };
    public List<GameObject> pieces = new List<GameObject> { };
}

