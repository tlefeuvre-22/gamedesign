using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public enum CellType
{
    land,
    water,
}
public class Cell : MonoBehaviour
{
    public GameObject Tree;
    public GameObject Rocks;
    public GameObject Chest;
    public GameObject Barrel;
    public GameObject SPowerUp;
    [SerializeField]
    public CellType type = CellType.land;
    public Material water;
    public Material land;
    public bool chest;
    public bool powerUp = false;
    public int _x;
    public int x
    {
        get => _x;
        set => _x = value;
    }
    public int _y;
    public int y
    {
        get => _y;
        set => _y = value;
    }
    [SerializeField]
    public int height;
    [SerializeField]
    public GameObject occupier;
    List<GameObject> collectible;
    bool wasInvoked = false;
    private void Start()
    {
        if(occupier!=null && occupier.TryGetComponent<EnemyPiece>(out EnemyPiece p))
        {
            p.coordinate[0] = x;
            p.coordinate[1] = y;
        }
    }
    public bool IsOccupied()
    {
        if (!wasInvoked)
        {
            collectible = new List<GameObject> { Chest, SPowerUp };
            wasInvoked = true;
        }
        return occupier != null && !collectible.Contains(occupier);
    }
    public bool IsFree()
    {
        return type == CellType.land && !IsOccupied();
    }
    public void ToggleChest()
    {
        if (occupier == Chest)
            occupier = null;
        else if (occupier != null)
            chest = false;
        else
        {
            chest = true;
            occupier = Chest;
            if (type == CellType.water)
                GetComponent<Renderer>().material = land;
            type = CellType.land;
        }
        DiplayObstacle();
    }
    public void ToggleSPowerUP()
    {
        if (occupier == SPowerUp)
        { 
            occupier = null;
            powerUp = false;
        }
        else if(occupier != null) {
            powerUp = false;
        }
        else
        {
            occupier = SPowerUp;
            powerUp = true;
            if (type == CellType.water)
                GetComponent<Renderer>().material = land;
            type = CellType.land;
        }
        DiplayObstacle();
    }
    public void Togglebarrel()
    {
        if (occupier == Barrel)
            occupier = null;
        else
        {
            occupier = Barrel;
            if (type == CellType.water)
                GetComponent<Renderer>().material = land;
            type = CellType.land;
        }
        DiplayObstacle();
    }
    #if (UNITY_EDITOR)
    public void ToggleEnnemy()
    {

        if (occupier != null && occupier.TryGetComponent<EnemyPiece>(out _))
        {
            for(var i=0; i< Board.Instance.Enemys.Count; i++)
            {
                if (Board.Instance.Enemys[i].GetComponent<EnemyPiece>().ID == occupier.GetComponent<EnemyPiece>().ID)
                {
                    Board.Instance.Enemys.RemoveAt(i);
                    break;
                }
            }
            Object.DestroyImmediate(occupier);
            occupier = null;
        }
        else if (occupier != null)
        { 
            occupier = null; 
        }
        else
        {
            if (type != CellType.water)
            {

                GameObject pref = Resources.Load("Prefabs/Enemy") as GameObject;
                GameObject enemy= PrefabUtility.InstantiatePrefab(pref) as GameObject;
                enemy.GetComponent<EnemyPiece>().Move(gameObject);
                enemy.GetComponent<EnemyPiece>().alredySpawn = true;
                enemy.GetComponent<EnemyPiece>().AddId();
                Board.Instance.Enemys.Add(enemy);
                occupier = enemy;
            }
        }
        DiplayObstacle();
    }
    #endif
    public void ToggleObstacle()
    {
        if (occupier != null)
            occupier = null;
        else
        {
            GetComponent<Renderer>().material = land;
            if (height == 0)
            {
                GetComponent<Renderer>().material = land;
                height = 1;
                type = CellType.land;
            }
            if (Random.Range(0, 2) == 0)
                occupier = Tree;
            else
                occupier = Rocks;
        }
        DiplayObstacle();
    }
    private void OnValidate()
    {
        if (type == CellType.land)
        { 
            GetComponent<Renderer>().material = land;
            if (height == 0)
                height = 1;
        }
        if (type == CellType.water)
        {
            GetComponent<Renderer>().material = water;
            occupier = null;
            height = 0;
        }
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
        DiplayObstacle();
    }
    public void DiplayObstacle()
    {
        Rocks.SetActive(occupier == Rocks);
        Tree.SetActive(occupier == Tree);
        Chest.SetActive(occupier == Chest);
        Barrel.SetActive(occupier == Barrel);
        SPowerUp.SetActive(occupier == SPowerUp);
    }
}
