using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    //TODO implem core
    public enum ActionType
    {
        nothing,
        moveAPiece,
        attack,
        deployPiece,
        specialAttack,
        waitForSel,
    }

    public List<Objects> inventory = new List<Objects>();
    new Camera camera;
    GameObject selection;
    public GUISkin skin = null;
    public ActionType act = ActionType.deployPiece;
    int nextD = 0;
    uint bonusPooints = 0;//TODO implement bonus point with certain ruel if you kill withe water

    void Start()
    {
        ObjApplyEffect();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        ChestManager.AssingCamera(camera);
    }
    public void ObjApplyEffect()
    {
        foreach (Objects obj in inventory)
        {
            obj.ApplyEffect();
            obj.Display();
        }

    }

    void Update()
    {
        Transform obj = OnClick();
        if (obj != null)
            Action(obj);
        if (Input.GetMouseButtonDown(1))
            CancelAct();
        TurnManager.UpdateTurn();
    }
    Transform OnClick()
    {
        if (Input.GetMouseButtonDown(0) && TurnManager.playerTurn)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                return hit.transform;
            }
        }
        return null;
    }
    
    public void attackMode()
    {
        if (act == ActionType.moveAPiece)
        {
            act = ActionType.attack;
            selection.GetComponent<PlayerPiece>().DisplayC();
        }
    }
    public void specialAttackMode()
    {
        if (act == ActionType.moveAPiece)
        {
            act = ActionType.specialAttack;
            //TODO display
        }
    }

    public void EndTurn()
    {
        TurnManager.EndTurn();
    }

    void CancelAct()
    {
        if (selection != null && act != ActionType.nothing)
        {
            selection.GetComponent<PlayerPiece>().ToggleSelect();
            selection.GetComponent<PlayerPiece>().ClearDisplay();
        }
        act = ActionType.nothing;
    }
    public void DeployPices(Transform cell)
    {
        Cell c = cell.GetComponent<Cell>();
        if (c.IsFree())
        {
            GameObject piece = Board.Instance.pieces[nextD];
            PlayerPiece p = piece.GetComponent<PlayerPiece>();
            p.Move(cell.gameObject);
            nextD++;
            if (nextD >= 3)
                act = ActionType.nothing;
        }
    }
    void Action(Transform objectHit)
    {
        if (act == ActionType.deployPiece)
        {
            DeployPices(objectHit);
        }
        if (act == ActionType.specialAttack)
        {
            selection.GetComponent<PlayerPiece>().specialAttack(objectHit.gameObject);
            CancelAct();
        }
        if (objectHit.CompareTag("Movables") && act == ActionType.nothing)
        {
            act = ActionType.moveAPiece;
            selection = objectHit.gameObject;
            selection.GetComponent<PlayerPiece>().DisplayMouv();
            selection.GetComponent<PlayerPiece>().ToggleSelect();

        }
        else if (act == ActionType.moveAPiece && objectHit.CompareTag("cell") && objectHit.GetComponent<Cell>().IsFree())
        {
            selection.GetComponent<PlayerPiece>().MoveTo(objectHit.gameObject);
            selection.GetComponent<PlayerPiece>().ClearDisplay();
            CancelAct();
        }
        else if (act == ActionType.attack && objectHit.CompareTag("cell"))
        {
            selection.GetComponent<PlayerPiece>().Attack(objectHit.gameObject);
            CancelAct();
        }
    }

}

