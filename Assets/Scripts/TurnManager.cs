using System;
using TMPro;
using UnityEngine;

public static class TurnManager
{
    static float time= 30 * 3;
    static readonly float defTime = 30*3;
    public static bool playerTurn = true;
    public static void EndTurn()
    {
        playerTurn = false;
        time = defTime;
    }
    public static bool ChekIfTF()
    {
        foreach (GameObject piece in Board.Instance.pieces)
        {
            PlayerPiece p = piece.GetComponent<PlayerPiece>();
            if (p.canAttack || p.mouvPoints != 0)
                return false;
        }
        return true;
    }
    static void PlayEnnemysTurn()
    {
        foreach (GameObject enemy in Board.Instance.Enemys)
        {
            try //TODO remove dead enemy from enemys
            {
                enemy.GetComponent<EnemyPiece>().PlayTurn();
            }
            catch(Exception e)
            {
                Debug.Log(e);
            }
        }
        foreach (GameObject piece in Board.Instance.pieces)
        {
            PlayerPiece p = piece.GetComponent<PlayerPiece>();
            p.canAttack = true;
            p.RestMovePt();
        }
        playerTurn = true;
    }
    public static void UpdateTurn()
    {
        if (time <= 0 || ChekIfTF())
        {
            EndTurn();
        }
        if (playerTurn)
        {
            time -= 1 * Time.deltaTime;
            GameObject.Find("timeDisplay").GetComponent<TMP_Text>().text = "Time: " + ((int)time).ToString();
        }
        else
        {
            PlayEnnemysTurn();
        }
    }


}
