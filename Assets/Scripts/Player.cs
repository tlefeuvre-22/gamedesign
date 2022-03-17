using TMPro;
using UnityEngine;
public class Player : MonoBehaviour
{
    enum actionType
    {
        nothing,
        moveAPiece,
        attack,
    }
    float time = 10;
    float defTime = 10;
    bool playerTurn = true;
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
        if (Input.GetMouseButtonDown(0) && playerTurn)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                action(hit.transform);
            }
        }
        if (Input.GetMouseButtonDown(1))
            cancelAct();
        updateTurn();
    }
    public void attackMode()
    {
        if (act == actionType.moveAPiece)
            act = actionType.attack;
    }
    public void endTurn()
    {

        playerTurn = false;
        time = defTime;
    }
    void updateTurn()
    {
        if (time <= 0)
        {
            playerTurn = false;
            time = defTime;
        }
        if (playerTurn)
        {
            time -= 1 * Time.deltaTime;
            GameObject.Find("timeDisplay").GetComponent<TMP_Text>().text = "Time: " + ((int)time).ToString();
        }
        else
        {
            playEnnemyTurn();
        }
    }
    void playEnnemyTurn()
    {
        foreach (GameObject enemy in Board.Instance.enemys)
        {
            try //TODO remove dead enemy from enemys
            {
                enemy.GetComponent<ePiece>().playTurn();
            }
            catch
            {
            }

        }
        foreach (GameObject piece in Board.Instance.pieces)
        {
            Piece p = piece.GetComponent<Piece>();
            p.canAttack = true;
            p.restMovePt();
        }
        playerTurn = true;
    }
    void cancelAct()
    {
        if (selection != null && act != actionType.nothing)
            selection.GetComponent<Piece>().toggleSelect();
        act = actionType.nothing;
    }
    void action(Transform objectHit)
    {
        if (objectHit.tag == "Movables" && act == actionType.nothing)
        {
            act = actionType.moveAPiece;
            selection = objectHit.gameObject;
            selection.GetComponent<Piece>().toggleSelect();
            
        }
        else if (act == actionType.moveAPiece && objectHit.tag == "cell")
        {
            selection.GetComponent<Piece>().moveTo(objectHit.gameObject);
            cancelAct();
        }
        else if (act == actionType.attack && (objectHit.tag == "cell" || objectHit.tag == "busyCell"))
        {
            selection.GetComponent<Piece>().attack(objectHit.gameObject);
            cancelAct();
        }
    }

}

