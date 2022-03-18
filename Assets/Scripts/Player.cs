using UnityEngine;
public class Player : MonoBehaviour
{
    enum actionType
    {
        nothing,
        moveAPiece,
        attack,
    }
    
    new Camera camera;
    GameObject selection;
    public GUISkin skin = null;
    actionType act = actionType.nothing;

    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) &&  TurnManager.playerTurn)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Action(hit.transform);
            }
        }
        if (Input.GetMouseButtonDown(1))
            CancelAct();
        TurnManager.UpdateTurn();
    }
    public void attackMode()
    {
        if (act == actionType.moveAPiece)
        { 
            act = actionType.attack;
            selection.GetComponent<PlayerPiece>().DisplayC();
        }
    }
    
    public void EndTurn()
    {
        TurnManager.EndTurn();
    }
   
    void CancelAct()
    {
        if (selection != null && act != actionType.nothing)
        { 
            selection.GetComponent<PlayerPiece>().ToggleSelect();
            selection.GetComponent<PlayerPiece>().ClearDisplay();
        }
        act = actionType.nothing;
    }
    void Action(Transform objectHit)
    {
        if (objectHit.CompareTag("Movables") && act == actionType.nothing)
        {
            act = actionType.moveAPiece;
            selection = objectHit.gameObject;
            selection.GetComponent<PlayerPiece>().ToggleSelect();
            
        }
        else if (act == actionType.moveAPiece && objectHit.tag == "cell")
        {
            selection.GetComponent<PlayerPiece>().MoveTo(objectHit.gameObject);
            CancelAct();
        }
        else if (act == actionType.attack && (objectHit.tag == "cell" || objectHit.tag == "busyCell"))
        {
            selection.GetComponent<PlayerPiece>().Attack(objectHit.gameObject);
            CancelAct();
        }
    }

}

