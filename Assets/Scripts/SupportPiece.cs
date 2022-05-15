using UnityEngine;

public class SupportPiece : PlayerPiece
{
    bool InSelMod = false;
    EnemyPiece e;
    private void Start()
    {
        
    }
    
    private void Update()
    {
        if (InSelMod)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.TryGetComponent(out Cell c))
                    {
                        if (c.IsFree())
                        {
                            e.MoveTo(hit.transform.gameObject, true);
                            InSelMod = false;
                            Camera.main.GetComponent<Player>().act = Player.ActionType.nothing;
                        }
                    }
                }
            }
        }
    }
    public override void specialAttack(GameObject cell)
    {
        /*GameObject target = cell.GetComponent<Cell>().occupier;

        if (target != null)
        {
            int x = cell.GetComponent<Cell>().x;
            int y = cell.GetComponent<Cell>().y;
            //if (CheckDistance(x, y, coordinate[0], coordinate[1], range))
            //jump par dessu obstacle
            if (x == coordinate[0] || y == coordinate[1])
            {
                e = target.GetComponent<EnemyPiece>();
                Camera.main.GetComponent<Player>().act = Player.ActionType.waitForSel;
                canAttack = false;
            }
        }*/
    }
}
