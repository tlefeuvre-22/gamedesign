using UnityEngine;

public class cell : MonoBehaviour
{
    private int _x;
    public int x
    {
        get => _x;
        set => _x = value;
    }
    private int _y;
    public int y
    {
        get => _y;
        set => _y = value;
    } 
    public GameObject occupier;
}
