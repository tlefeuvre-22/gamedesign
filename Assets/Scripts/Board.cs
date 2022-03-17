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
    public GameObject[] enemys = new GameObject[1];
    public GameObject[] pieces = new GameObject[1];
}

